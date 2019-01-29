using Hunt.Model;
using System.Collections.Generic;

namespace Hunt.ServiceContext
{
    public class ServiceContext : IServiceContext{

        public IEnumerable<User> Users { get; private set; }

        public ServiceContext()
        {
            
        }


    }

    //pisanie testowe

    public class ProtocolHeader {
        public byte RecipientIdentifier { get; private set; }
        public byte SectionAmount {get; private set; } // ilość sekcji

        //public byte 
    }


    public class ProtocolReader {

        public void Read(){
            
            // using(BinaryReader reader = new BinaryReader()){
                
            //     reader.

            // }



        }



    }

    public interface IConnection{
        void Send();
    }




}