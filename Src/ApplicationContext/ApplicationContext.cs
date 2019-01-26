using System;
using System.Collections.Generic;
using System.IO;
using HuntingHelperWebService.Model;

namespace HuntingHelperWebService.ApplicationContext{
    public class ApplicationContext : IApplicationContext{

        public IEnumerable<User> Users { get; private set; }

        public ApplicationContext()
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