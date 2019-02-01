using Hunt.Model;
using Hunt.ServiceContext;
using System.Collections.Generic;

namespace Hunt.ApplicationContext
{
    public class ServiceContext : IServiceContext{
  
        public void CreateSession(User user){

        }

        public void DestoySession(User user){
            
        }
    }
}



//  //pisanie testowe

//     public class ProtocolHeader {
//         public byte RecipientIdentifier { get; private set; }
//         public byte SectionAmount {get; private set; } // ilość sekcji

//         //public byte 
//     }


//     public class ProtocolReader {

//         public void Read(){
            
//             // using(BinaryReader reader = new BinaryReader()){
                
//             //     reader.

//             // }



//         }



//     }

//     public interface IConnection{
//         void Send();
//     }



