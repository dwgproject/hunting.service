using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using GravityZero.HuntingSupport.Service.Context.Exceptions;

namespace GravityZero.HuntingSupport.Service.Session
{
    public class UserSession : IUserSession
    {
        private ConcurrentDictionary<Guid, SessionUnit> sessions;

        public UserSession()
        {
            sessions = new ConcurrentDictionary<Guid, SessionUnit>();
            Task.Run(() => CheckSessions());
        }

        public void AddOrUpdate(SessionUnit session)
        {
             if (sessions.ContainsKey(session.Identifier)){
                SessionUnit temporary;
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
                SessionUnit temporary;
                bool isSucces = sessions.TryRemove(identifier, out temporary);
                if (!isSucces)
                    throw new SessionCloseException("Error occured during closing the session.");
            }
        }

        public SessionUnit Get(Guid identifier)
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
                SessionUnit current = session.Value;
                TimeSpan delta = now.Subtract(current.LastTick);
                if (delta.Minutes > 20)
                    Close(session.Key);
            }
            Task.Delay(5000);
        }
    }
}