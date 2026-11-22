namespace Second_Project_Test__Attempt_2_
{
    public class usersList
    {
        private string username;
        private string password;

        public usersList(string username, string password)
        {
            this.username = username.Trim();
            this.password = password.Trim();
        }

        public string getUsername()
        {
            return username;
        }

        public string getPassword()
        {
            return password;
        }
    }
}
