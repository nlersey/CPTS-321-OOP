//Programmer: Nicholas Lersey 11633967
using System;


namespace CPTS321
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            // The main function to convert infix expression
            //to postfix expression
            //string infixToPostfix(string s)
            //{
            //    std::stack<char> st;
            //    st.push('N');
            //    int l = s.length();
            //    string ns;
            //    for (int i = 0; i < l; i++)
            //    {
            //        // If the scanned character is an operand, add it to output string.
            //        if ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z'))
            //            ns += s[i];

            //        // If the scanned character is an ‘(‘, push it to the stack.
            //        else if (s[i] == '(')

            //            st.push('(');

            //        // If the scanned character is an ‘)’, pop and to output string from the stack
            //        // until an ‘(‘ is encountered.
            //        else if (s[i] == ')')
            //        {
            //            while (st.top() != 'N' && st.top() != '(')
            //            {
            //                char c = st.top();
            //                st.pop();
            //                ns += c;
            //            }
            //            if (st.top() == '(')
            //            {
            //                char c = st.top();
            //                st.pop();
            //            }
            //        }

            //        //If an operator is scanned
            //        else
            //        {
            //            while (st.top() != 'N' && prec(s[i]) <= prec(st.top()))
            //            {
            //                char c = st.top();
            //                st.pop();
            //                ns += c;
            //            }
            //            st.push(s[i]);
            //        }
            //    }
            //    //Pop all the remaining elements from the stack
            //    while (st.top() != 'N')
            //    {
            //        char c = st.top();
            //        st.pop();
            //        ns += c;
            //    }

            //    return ns;
            //}




        }
    }
}
