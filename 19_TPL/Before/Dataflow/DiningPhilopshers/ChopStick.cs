namespace DiningPhilopshers
{
    public class ChopStick
    {
        private readonly int id;

        public ChopStick(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return string.Format("Chopstick Id: {0}", id);
        }
    }
}