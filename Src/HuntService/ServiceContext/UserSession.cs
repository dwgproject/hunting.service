using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Hunt.ServiceContext.Exceptions;

namespace Hunt.ServiceContext.ServiceSession{
    public class UserSession : IUserSession
    {
        private ConcurrentDictionary<Guid, Session> sessions;

        public UserSession()
        {
            sessions = new ConcurrentDictionary<Guid, Session>();
            Task.Run(() => CheckSessions());
        }

        public void AddOrUpdate(Session session)
        {
             if (sessions.ContainsKey(session.Identifier)){
                Session temporary;
                bool isSuccess = sessions.TryRemove(session.Identifier, out temporary);
                if (!isSuccess)
                    throw new SessionCloseException("Error occured during updating the session.");
                isSuccess = sessions.TryAdd(session.Identifier, session);
                if (!isSuccess)
                    throw new SessionCloseException("Error occured during updating the session.");
            }else{
                bool isSuccess = sessions.TryAdd(session.Identifier, session);
                if (!isSuccess)
                    throw new SessionCloseException("Error occured during adding the session.");
            }
        }

        public void Close(Guid identifier)
        {
            if (sessions.ContainsKey(identifier)){
                Session temporary;
                bool isSucces = sessions.TryRemove(identifier, out temporary);
                if (!isSucces)
                    throw new SessionCloseException("Error occured during closing the session.");
            }
        }

        public Session Get(Guid identifier)
        {
            if (sessions.ContainsKey(identifier)){
                return sessions[identifier];
            }
            return null;
        }
        //ticka ustawia tick (http request od klienta) jeżeli lastTisk jest większy niż 20 minut to usuwam
        private void CheckSessions(){
            DateTime now = DateTime.Now;
            foreach (var session in sessions.ToArray()){
                Session current = session.Value;
                TimeSpan delta = now.Subtract(current.LastTick);
                if (delta.Minutes > 20)
                    Close(session.Key);
            }
            Task.Delay(5000);
        }
    }
}