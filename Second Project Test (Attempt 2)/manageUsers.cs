using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Second_Project_Test__Attempt_2_
{
    public static class manageUsers
    {
        private static readonly string filePath = "users.txt";

        public static List<usersList> loadUsers()
        {
            var users = new List<usersList>();
            try
            {
                if (File.Exists(filePath))
                {
                    foreach (var line in File.ReadLines(filePath))
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            users.Add(new usersList(parts[0], parts[1]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return users;
        }

        public static bool saveUser(usersList user)
        {
            try
            {
                File.AppendAllText(filePath, user.getUsername() + "|" + user.getPassword() + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static usersList findUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            foreach (var user in loadUsers())
            {
                if (user.getUsername().Equals(username))
                    return user;
            }

            return null;
        }
    }
}
