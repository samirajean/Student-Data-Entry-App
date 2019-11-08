using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_app
{
    public partial class Form1 : Form
    {
        //if the number of students is knows e.g. 100 it's better to use an array of student
        List<Student> s = new List<Student>(); //linkedlist has functions like add and remove
        int position; //this is the element I am standing on

        public Form1() //same class name, have no data type-> constructor
        {
            InitializeComponent();
            position = -1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Add student
        {
            Student tmp = new Student(); //ba-create new student, reverse makan leih, tmp bt-represent el student
            //badkhal l info bt3at l student
            tmp.name = textBox1.Text;
            //bec of parsing an error could occur if I entered a value rather than a number in gpa
            try
            {
                tmp.gpa = double.Parse(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("You have entered an invalid GPA value, please enter a valid value.");
                textBox2.Clear();
                return;
            }
            tmp.term = int.Parse(textBox3.Text);
            s.Add(tmp); //adding student to list, student's info are saved in RAM
            //bamsah l textbox b3d madkhal l info bta3t l student 3ashan a2dr adkhl info l student tany lw 3ayz a3mlo add
            /*textBox1.Clear();
              textBox2.Clear();
              textBox3.Clear();*/
            //to use for loop to clear
            //foreach used to looping in array, string e.g. to loop an array as x[100] -> foreach(int p in x) such that p is a pointer 
            //bt7ded no3 l haga elly 3ayz t-loop 3aleha fel example dah kant int, fel design bta3 el entry da kan controls
            //controls is an array contains every element in your form deisgn, control noo3 el element l wa7da, b is a pointer
            foreach (Control t in Controls) //lw makontsh 3arf control deh momken astkhdem object 
            {
                //we need to check if the element is textbox or not to be cleared
                if (t is TextBox)
                {
                    (t as TextBox).Clear();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student max = new Student();
            max = s.ElementAt(0); //suppose that the max gpa is the first student
            foreach (Student st in s) //hamshy 3al list bt3ty elly heya no3ha student
            {
                if (st.gpa > max.gpa)
                {
                    max = st;
                }

            }
            //showing the max gpa
            MessageBox.Show("Name: " + max.name + "\nGPA: " + max.gpa + "\nTerm: " + max.term);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog a = new SaveFileDialog();
            a.ShowDialog();
            //stream howa el haga el haktb 3aleha l haga, console window, file, printer etc. has 2 function ReadLine & WriteLine
            System.IO.StreamWriter f = new System.IO.StreamWriter(a.FileName); //I defined a stream called f w rabto bel file
            //f.WriteLine("");
            //write code to enter the data of students into file
            foreach (Student p in s)
            {
                f.WriteLine(p.name);
                f.WriteLine(p.gpa.ToString()); //bahawelo mn double to strin
                f.WriteLine(p.term + ""); //method 2 concatenate int to string hay7welha string
            }
            f.Close();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();
            System.IO.StreamReader myfile = new System.IO.StreamReader(o.FileName);
            s.Clear(); //ba-clear l list
            //read first entry in file
            string tmp = myfile.ReadLine(); //b2ra awel satr bs, lw kan l file fady hyrg3 null lw maknsh fady bakush fel while loop
            //if the current entry is not null
            while (tmp != null) //looping lehad l end of file, lw weslt l null yeb2a an f akher l file
            {
                Student data = new Student(); //data ba-store feiha l data bta3t l students
                data.name = tmp;
                tmp = myfile.ReadLine();
                data.gpa = double.Parse(tmp);
                tmp = myfile.ReadLine();
                data.term = int.Parse(tmp);
                tmp = myfile.ReadLine();
                s.Add(data);
            }
            myfile.Close();
        }
        //policy of next & previous
        //1. if text boxes are empty-> Next: get the first element, Previous: shows last element in list
        //2. if reach 1st element, prev produces message 1st element
        //3. if reach last element, next produces message
        //hastkhdem counter type int, w initially = -1 eftrad en mafesh haga gowa l list

        private void display() //method to display data of student in next & previous
        {
            Student tmp = s.ElementAt(position); //define tmp student to store data of student
            textBox1.Text = tmp.name;
            textBox2.Text = tmp.gpa + "";
            textBox3.Text = tmp.term + "";
        }
        //this is the code for previous
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                position = s.Count - 1; //ro7 l akher element
            else
                position--;
            if (position == -1)//lw kont 3nd 0 w na2st 1 (--)
            {
                position = s.Count - 1;
            }

            display();

        }

        //this is the code for next
        private void button3_Click(object sender, EventArgs e)
        {
            //to check if the textboxes are empty, no data
            if (textBox1.Text == "")
                position = 0; //I am at the first element
            else
                position++;
            if (position == s.Count) //if the poisiton = number of element, khaleh yerg3 tany l awel element (circular)
                position = 0; //return back to the first element
            /*momken akhleh ytl3 message ene weslt l akher l entries lw weslt l akher student
            //MessageBox.Show("This is the end of the entries");
            //foreach(control c in Controls)
            {
                if (c is TextBox)
                    (c as TextBox).Clear();
            }
           return;*/
            display();
            
        }

        //this is the code for delete, policy: delete the data of the poisiton I am at
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please select element to delete first!");
                return;
            }
            else
            {
                s.RemoveAt(position); //poisiton hategy mn next w previous
                foreach (Control c in Controls)
                {
                    if (c is TextBox)
                        (c as TextBox).Clear();

                }
            }
        }


        

    }
}
