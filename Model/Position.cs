namespace HuntingHelperWebService.Model{
    public class Position{
        public int X {get; private set;}
        public int y {get;private set;}

        public Position(int x, int y)
        {
            this.X = x;
            this.y = y;
        }
    }
}