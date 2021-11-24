using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Gamestate
    int level;
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }
    
    void ShowMainMenu()
    {
        string greeting = "Hello User";
        Terminal.ClearScreen();
        currentScreen = Screen.MainMenu;
        level = 0;
        password = "";
        Terminal.WriteLine(greeting);
        Terminal.WriteLine("Initializing Hacking...\n\n");
        Terminal.WriteLine("Hacking into...");
        Terminal.WriteLine("Write 1 for: \"Local Hospital\"");
        Terminal.WriteLine("Write 2 for: \"Police Station\"");
        Terminal.WriteLine("Write 3 for: \"The Bank\"");
        Terminal.WriteLine("Write \"Menu\" to go back\n\n");
        Terminal.WriteLine("Enter IP-address: ");
    }

    void OnUserInput(string input)
    {
        if (IsInputValid(input))
        {
            RunGame(input);
        }
    }

    void RunGame(string input)
    {
        if(input.ToLower() == "menu")
        {
            ShowMainMenu();
        }
        else if(currentScreen == Screen.MainMenu)
        {
            ChooseLevel(input);
        }
        else if (currentScreen == Screen.Password)
        {
            GuessPassword(input);
        }
        else if (currentScreen == Screen.Win)
        {
            ShowMainMenu();
        }

    }

    bool IsInputValid(string input)
    {
        if(input.ToLower() == "menu")
        {
            return true;
        }
        else if (currentScreen == Screen.MainMenu)
        {
            if (input == "1" || input == "2" || input == "3")
            {
                return true;
            }
            Terminal.WriteLine("Bad IP-Address... Try again: ");
            return false;
        }
        else if (currentScreen == Screen.Password)
        {
            if (input.Length < 3)
            {
                Terminal.WriteLine("*Password too short*");
                return false;
            }
            else if (input.Length > 15)
            {
                Terminal.WriteLine("*Password too long*");
                return false;
            }
        }
        return true;
    }

    void ChooseLevel(string input)
    {
        currentScreen = Screen.Password;
        if (input == "1")
        {
            level = 1;
            Terminal.ClearScreen();
            Terminal.WriteLine("Hacking into: \"Local Hospital\"...");
        }
        else if (input == "2")
        {
            level = 2;
            Terminal.ClearScreen();
            Terminal.WriteLine("Hacking into: \"Police Station\"...");
        }
        else if (input == "3")
        {
            level = 3;
            Terminal.ClearScreen();
            Terminal.WriteLine("Hacking into: \"The Bank\"...");
        }
        Terminal.WriteLine("...\n...\nConnection Established...\n\nEnter Password: ");
        setPassword();
    }

    void GuessPassword(string input)
    {
        if(input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Wrong Password. Try again");
            Terminal.WriteLine("Hint: " + CreateHint());
        }
    }

    string CreateHint()
    {
        char[] passwordCopy = password.ToCharArray(); // Convert string to char array
        for (int i = password.Length - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            char temp = passwordCopy[i];
            passwordCopy[i] = passwordCopy[rnd];
            passwordCopy[rnd] = temp;
        }
        string hint = new string(passwordCopy);
        return hint;
    }

    void setPassword()
    {
        if(level == 1)
        {
            string[] passwords = { "sick", "doctor", "nurse", "vaccine" };
            int rnd = Random.Range(0, passwords.Length-1);
            password = passwords[rnd];
        }
        else if (level == 2)
        {
            string[] passwords = { "officer", "criminals", "prison", "passport", "patrol", "sirens" };
            int rnd = Random.Range(0, passwords.Length);
            password = passwords[rnd];
        }
        else if (level == 3)
        {
            string[] passwords = { "commission", "interest", "mortgage", "statement", "unsecured", "withdrawal", "depositor" };
            int rnd = Random.Range(0, passwords.Length);
            password = passwords[rnd];
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        Terminal.WriteLine("Successfully Logged In!");
        switch(level){
            case 1:
                Terminal.WriteLine(@"
 |___|________|_
 |___|________|_|-----
 |   |        |       .
                    .
                      .
                    .
                    ");
                break;
            case 2:
                Terminal.WriteLine(@"
   ,   /\   ,
  / '-'  '-' \
  |  POLICE  |
  |   .--.   |
  |  ( 19 )  |
  \   '--'   /
   '--.  .--'
       \/
                ");
                break;
            case 3:
                Terminal.WriteLine(@"
  ooo,    .---.
 o`  o   /    |\________________
o`   'oooo()  | ________   _   _)
`oo   o` \    |/        | | | |_
  `ooo'   `---'         \_/ \___|
                    ");
                break;
        }
    }
}
