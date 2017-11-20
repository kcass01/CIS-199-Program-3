//Grading ID: A5196
//Program Number: Program 3
//Due Date: 13 April 2017
//Course Section: CIS 199-01
//This program will tell a user when ther register for courses.
//The user will select from 1 of 4 radio buttons to indicate their class status.
//The program will then use their first initial and class to assess the date when the register.
//It is based off of parallel arrays rather than if statements.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }
        //This method will assign a time to the registering student, when certains factors inputted.
        //preconditions: Parallel arrays of char letters and string times, and a last Name initial char.
        //postconditions: returns the time, as a string.
        public string TimeFinder(char[] letters, string[] times, char lastNameLetterCh)
        {
            bool found = false;//Stores a true/false of if the value is found
            string timeString = "None Found"; //Holds the string of the time output
            //Since lower limits are used, serach starts from end
            for(int index = letters.Length-1 ; index >= 0 && !found ; index--)
            {
                //If the Character is found, will stop the loops and assign a time.
                if(lastNameLetterCh >= letters[index])
                {
                    found = true;
                    timeString = times[index];
                }
            }
            return timeString;
        }

        // Find and display earliest registration time
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "March 29";  // 1st day of registration
            const string DAY2 = "March 30";  // 2nd day of registration
            const string DAY3 = "March 31";  // 3rd day of registration
            const string DAY4 = "April 3";   // 4th day of registration
            const string DAY5 = "April 4";   // 5th day of registration
            const string DAY6 = "April 5";   // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;
                        //Array of the upper class letters, sorted aphabetically
                        char[] upperClassLetters = {'A', 'E', 'J', 'P', 'T'};
                        //Array of the upper class times, sorted by what letterthe correlate with
                        string[] upperClassTimes = { TIME3, TIME4, TIME5, TIME1, TIME2};

                        //This passes the letters and times to the method
                        timeStr = TimeFinder(upperClassLetters, upperClassTimes, lastNameLetterCh);

                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // C-O on one day
                            if ((lastNameLetterCh >= 'C') && // >= C and
                                (lastNameLetterCh <= 'O'))   // <= O
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // C-O on one day
                            if ((lastNameLetterCh >= 'C') && // >= C and
                                (lastNameLetterCh <= 'O'))   // <= O
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }
                        //Array of lower class letters, sorted in alphabetic order.
                        char[] lowerClassLetters = { 'A', 'C', 'E','G', 'J', 'M', 'P','R', 'T', 'W' };
                        //Array of lower class times, correlating with what letter has that time.
                        string[] lowerClassTimes = { TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4 };

                        //This will pass lower class letters and times to the method
                        timeStr = TimeFinder(lowerClassLetters, lowerClassTimes, lastNameLetterCh);

                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
}
