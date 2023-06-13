namespace FunctionApp1.Helpers
{
    public class Credentials
    {
        public string consumerKey { get; private set; }
        public string consumerSecret { get; private set; }
        public string accessToken { get; private set; }
        public string accessTokenSecret { get; private set; }

        public Credentials()
        {
            /*
            Claves de Manu 
            consumerKey = "oaHmklWQpTTnZbkF8nKrECuCB";
            consumerSecret = "ZWNBcC34hJpvZXEeBEUvHAvUyFElxLTEfxuSWljrAJqNnNftso";
            accessToken = "3386576481-nAP33v2JZ3ITsr6bfIyutuGvZkwddBleg7hPr5a";
            accessTokenSecret = "1nlu1RhY870JqIrvwayp4Z9eG34W8jityEkvycQvefuFC";*/


            //Claves del grupo
            consumerKey = "szIBvMFejQh1MvmxkfILJQcpY";
            consumerSecret = "jI70TjG1sPQMwJksAHULxHgc0zbAbAQkTw66X1vR1ODMa1Bn6C";
            accessToken = "1663672719030771714-fVE2vCwHpCcr18ddJ6rCJz7nAt5a62";
            accessTokenSecret = "aVH2gLYPSAVYCPg2pOAkVPiasvuVWfXCodtQuh6lIyavQ";
        }
    }
}
