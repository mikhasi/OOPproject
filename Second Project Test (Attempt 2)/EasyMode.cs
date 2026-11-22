namespace Second_Project_Test__Attempt_2_
{
    public class EasyMode : GameMode
    {
        public override int CalculatePoints(int time)
        {
            if (time < 20) return 5;
            if (time < 40) return 4;
            if (time < 60) return 3;
            if (time < 80) return 2;
            return 1;
        }
    }
}
