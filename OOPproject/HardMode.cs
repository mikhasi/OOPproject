namespace Second_Project_Test__Attempt_2_
{
    public class HardMode : GameMode
    {
        public override int CalculatePoints(int time)
        {
            if (time < 30) return 5;
            if (time < 60) return 4;
            if (time < 90) return 3;
            if (time < 120) return 2;
            return 1;
        }
    }
}
