﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Global;

namespace Database_Project
{

	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			err_label.Text = "";
		}

		private void login_button_Click(object sender, EventArgs e)
		{
           // Verify password
            List<Dictionary<string, object>> users = SQL.ReadQuery($"SELECT * FROM USERS WHERE user_username = '{username_textBox.Text}'");

			err_label.Text = "";
			// If user not found
			if (users.Count == 0)
			{
				err_label.Text = "Wrong username";
			}
			// If wrong password
			else if (users[0]["user_password"].ToString() != password_textBox.Text)
			{
				err_label.Text = "Wrong password";
			}
			// Everything good
			else
			{
				Session.userID = (int)users[0]["user_id"];
				Session.userFirstName = users[0]["user_firstname"].ToString();
                Session.username = users[0]["user_username"].ToString();
                Session.password = users[0]["user_password"].ToString();
                Session.userlastname = users[0]["user_lastname"].ToString();
                Session.email = users[0]["user_email"].ToString();
                Session.creditcard = users[0]["user_creditcard"].ToString();
				Session.userBalance = (double)users[0]["user_balance"];

				// Hide login window
				Hide();

				// Load MainWindow
				MainWindow main = new MainWindow();

				// This line tells MainWindow to close the current window (login window)
				// with it when it closes, otherwise the application will keep running in
				// the background with the login window hidden
				main.FormClosed += (s, args) => Close();

				main.Show();
			}
        }

		private void register_button_Click(object sender, EventArgs e)
		{
			// Show registration form
			RegisterForm regForm = new RegisterForm();
			regForm.Show();
		}

		private void password_textBox_KeyDown(object sender, KeyEventArgs e)
		{
			// If the pressed key is Enter
			if (e.KeyCode == Keys.Enter)
			{
				login_button_Click(this, new EventArgs());
			}
		}

        private void admin_button_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, object>> admins = SQL.ReadQuery($"SELECT * FROM ADMIN WHERE admin_username = '{username_textBox.Text}'");

            err_label.Text = "";
            // If user not found
            if (admins.Count == 0)
            {
                err_label.Text = "Wrong username";
            }
            // If wrong password
            else if (admins[0]["admin_password"].ToString() != password_textBox.Text)
            {
                err_label.Text = "Wrong password";
            }
            // Everything good
            else
            {
                Session.adminID = (int)admins[0]["admin_id"];
                Session.adminFirstName = admins[0]["admin_firstname"].ToString();
                

                // Hide login window
                Hide();

                // Load MainWindow
               AdminWindow admin = new AdminWindow();

                // This line tells MainWindow to close the current window (login window)
                // with it when it closes, otherwise the application will keep running in
                // the background with the login window hidden
                admin.FormClosed += (s, args) => Close();

                admin.Show();
            }
        }
    }
}
