using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        int msg_count = 0;
        TcpClient tcpClient;// = new TcpClient();
        UdpClient client; //= new UdpClient();
        //UdpClient reciever;
        static NetworkStream stream;
        //const int LOCALPORT = 8001; // порт дл€ приема сообщений

        public static string ServerIP;
        const int REMOTEPORT = 8001; // порт дл€ отправки сообщений
        public static int Port = REMOTEPORT;
        IPAddress address;
        bool alive = false; // будет ли работать поток дл€ приема
        string[] users, nicknames, addrs, ports;
        //string[] nicknames = new string[users.Length];
        /*IPAddress*/
        string addr, sender_name;
        //bool isReaded = false;
        Dictionary<string, bool> isReadied = new Dictionary<string, bool>();
        bool LastReadied = false;
        bool state = false;

        AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        List<string[]> nodes = new List<string[]>(); 

        public Form1()
        {
            InitializeComponent();
            address = Dns.GetHostAddresses(Dns.GetHostName()).First<IPAddress>(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            if (Directory.Exists(@"history"))
            {
                File.Delete(@".log");
                string[] histories = Directory.GetFiles(@"history");
                foreach (var history in histories)
                {
                    ListViewItem user = new ListViewItem();
                    user.Text = history.Remove(0, history.LastIndexOf('\\') + 1).Split('.')[0];
                    //user.Text = history.Remove(0, history.LastIndexOf('\\') + 1).Split('.')[0];
                    
                    listView1.Items.Add(user);
                    isReadied.Add(user.Text, false);
                }
            }
            getUsers();
            Listening();
            field.Enabled = false;
            button3.Enabled = false;
            //Task.Run(ReceiveMessages);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 0; i++)
            //{

            //}
            //string text = "Hello, world! Hello, world! Hello, world! Hello, world! Hello, world! ";
            //Label r_message = new Label();
            //r_message.BackColor = Color.White;
            //r_message.Text = "Hello, world! Hello, world! Hello, world! Hello, world! Hello, world! ";

            //r_message.Dock = DockStyle.Right;
            //r_message.Dock = DockStyle.Right;




        }

        private void WriteMessage(string text, bool saveInFile, bool state)
        {
            if (state)
            {
                Label message = new Label();
                message.BackColor = Color.White;
                message.Text = text;
                message.Location = new Point(0, msg_count * 30);
                message.AutoSize = true;
                message.ContextMenuStrip = contextMenuStrip1;
                copyToolStripMenuItem.Click += copyMenuItem_Click;

                //TODO: јлгоритм переноса текста сообщени€ (плохо работает, нуждаетс€ в доработке)
                //if (message.Text.Length > 20)
                //{
                //    message.Text = message.Text.Insert((20 / 2) - 1, "-\n");
                //}
                panel1.Controls.Add(message);
                msg_count++;
            }
            if (saveInFile)
            {
                Directory.CreateDirectory(@"history");
                File.AppendAllLines($@"history/{sender_name}.log", new[] { text });
            }
        }
        //private void button3_Click(object sender, EventArgs e)
        //{

        //    WriteMessage(field.Text);
        //}

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            getUsers();
            if (nicknames.Contains(username.Text))
                MessageBox.Show("¬ы уже зарегистрированы!");
            else
            {

                tcpClient = new TcpClient();
                string[] data = new string[2];
                data[0] = address.ToString();
                //data[1] = localPort.Text;//LOCALPORT.ToString();
                data[1] = Port.ToString();//REMOTEPORT.ToString();
                var requestData = Encoding.UTF8.GetBytes(username.Text + '@' + String.Join(":", data));
                //tcpClient = new TcpClient();
                tcpClient.Connect(IPAddress.Parse(ServerIP), 9090);
                var stream = tcpClient.GetStream();
                stream.Write(requestData, 0, requestData.Length);
                tcpClient.Close();
            }
            ////IPAddress hostname = Dns.GetHostAddresses(Dns.GetHostName())[1].IsIPv6LinkLocal ? Dns.GetHostAddresses(Dns.GetHostName())[Int32.Parse(Console.ReadLine())] : Dns.GetHostAddresses(Dns.GetHostName())[0];
            //IPAddress address = Dns.GetHostAddresses(Dns.GetHostName()).First<IPAddress>(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            ////Console.WriteLine(hostname);
            //var requestData = Encoding.UTF8.GetBytes("get_users");
            //await stream.WriteAsync(requestData, 0, requestData.Length);
            //var responseData = new byte[512];
            //var response = new StringBuilder();
            //int bytes = 0;
            //do
            //{
            //    bytes = await stream.ReadAsync(responseData, 0, responseData.Length);
            //    response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
            //} while (bytes > 0);
            //Console.WriteLine(response);
            //tcpClient.Close();
        }
        private void Parse(string list)
        {
            users = list.Split("\n");
            nicknames = new string[users.Length];
            addrs = new string[users.Length];
            try
            {
                for (int i = 0; i < users.Length; i++)
                {
                    nicknames[i] = users[i].Split('@')[0];
                    addrs[i] = users[i].Split('@')[1];
                }
            }
            catch /*(Exception)*/
            {

                //throw;
            }
        }
        private void getUsers/*_Click*/(/*object sender, EventArgs e*/)
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(IPAddress.Parse(ServerIP), 9090);

                var stream = tcpClient.GetStream();
                var requestData = Encoding.UTF8.GetBytes("get_users");
                stream.Write(requestData, 0, requestData.Length);
                var responseData = new byte[512];
                var response = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(responseData, 0, responseData.Length);
                    response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
                } while (bytes > 0);

                File.WriteAllText(@"nodelist", response.ToString());
                Parse(response.ToString());

            }
            catch
            {
                MessageBox.Show("Ќет подключени€ к серверу. ¬ыполн€етс€ чтение с файла");

                try
                {
                    Parse(File.ReadAllText(@"nodelist"));
                

            //WriteMessage(response.ToString());
            //AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(nicknames);
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tcpClient.Close();
                }
                catch { MessageBox.Show("Ќет списка узлов!"); }
            }

        }

        //private void connect_Click(object sender, EventArgs e)
        //{

        //    // присоедин€емс€ к групповой рассылке
        //    client = new UdpClient(/*Int32.Parse(remotePort.Text)*/);
        //    // запускаем задачу на прием сообщений
        //    Task.Run(ReceiveMessages);

        //    // отправл€ем первое сообщение о входе нового пользовател€

        //    string message = address.ToString(); //userName + $" ({address}) вошел в чат";
        //    byte[] data = Encoding.Unicode.GetBytes(message);
        //    client.Send(data, data.Length, addrs[Array.IndexOf(nicknames, textBox1.Text)].Split(':')[0], Int32.Parse(remotePort.Text));
        //    //Task receiveTask = new Task(ReceiveMessages);
        //    //receiveTask.Start();

        //    //loginButton.Enabled = false;
        //    //logoutButton.Enabled = true;
        //    //sendButton.Enabled = true;
        //}

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    localPort.Enabled = checkBox1.Checked;
        //    remotePort.Enabled = checkBox1.Checked;
        //    if (checkBox1.Checked)
        //    {

        //        localPort.Text = "4004";
        //        remotePort.Text = "4005";
        //    }
        //    else
        //    {
        //        localPort.Text = LOCALPORT.ToString();
        //        remotePort.Text = REMOTEPORT.ToString();
        //    }

        //}
        async void ReceiveMessages()
        {
            //reciever = new UdpClient(Int32.Parse(localPort.Text));
            alive = true;
            try
            {
                while (alive)
                {

                    IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0); //null;
                    byte[] data = client.Receive(ref remoteIp);
                    string message = Encoding.Unicode.GetString(data);
                    //WriteMessage(message);
                    string name = message.Split(':')[0];
                    sender_name = name;
                    if (!nodes.Contains(SetAddress(name))) 
                        nodes.Add(SetAddress(name));
                    //addr = addrs[Array.IndexOf(nicknames, name)].Split(':')[0];
                    //Match match = Regex.Match(message, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                    //if (match.Success)
                    //    addr = IPAddress.Parse(message);
                    /*if(((*/
                    //IPAddress.TryParse(message, out addr);//){
                    //MessageBox.Show(addr.ToString());
                    //}
                    // добавл€ем полученное сообщение в текстовое поле
                    this.Invoke(new MethodInvoker(() =>
                    {
                        string time = DateTime.Now.ToShortTimeString();
                        WriteMessage($"[{time}]  {message}", true, state);//\r\n");

                    }));


                }
            }
            catch (ObjectDisposedException)
            {
                if (!alive)
                    return;
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void sendButton_Click(object sender, EventArgs e)
        {


            client = new UdpClient(/*Int32.Parse(remotePort.Text)*/);
            // запускаем задачу на прием сообщений
            //if (username.Text != "")
            //{
            //    nicknames[Array.IndexOf(nicknames, username.Text)] = "";
            //    source.Clear();
            //    source.AddRange(nicknames);
            //    textBox1.AutoCompleteCustomSource = source;
            //}
            if (username.Text == "")
                MessageBox.Show("¬ведите им€!");
            else if (!nicknames.Contains(username.Text))
            {
                MessageBox.Show("Ќет зарегистрированного пользовател€ с таким именем!");
            }
            else
            {

                // отправл€ем первое сообщение о входе нового пользовател€
                string message = String.Format("{0}: {1}", username.Text, field.Text);
                //string message = field.Text; //userName + $" ({address}) вошел в чат";
                byte[] data = Encoding.Unicode.GetBytes(message);
                try
                {

                    client.Send(data, data.Length, ports[0], Int32.Parse(ports[1]));
                    field.Clear();
                    string time = DateTime.Now.ToShortTimeString();
                    WriteMessage($"[{time}]  {message}", true, state);//\r\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ѕолучатель не известен!\n" + ex.Message);

                }




            }
            //WriteMessage(message + "\r\n");
            //WriteMessage(ports[1]);
            //Task.Run(ReceiveMessages);
        }
        private string[] SetAddress(string nick)
        {
            ports = addrs[Array.IndexOf(nicknames, nick)].Split(':');
            //WriteMessage(ports[2]);         
            //localPort.Text = ports[2];
            //remotePort.Text = ports[1];
            return addrs[Array.IndexOf(nicknames, nick)].Split(':');
        }
        private void button1_Click(object sender, EventArgs e)
        {

            SetAddress(textBox1.Text);
        }

        private void Listening()
        {
            client = new UdpClient(Port);
            //button5.Enabled = false;
            Task.Run(ReceiveMessages);
        }
        //private void button5_Click(object sender, EventArgs e)
        //{

        //    //reciever = new UdpClient(localPort);
        //    client = new UdpClient(Port);
        //    button5.Enabled = false;
        //    Task.Run(ReceiveMessages);

        //}



        private void getAddress_Click(object sender, EventArgs e)
        {
            bool is_exist = false;
            ListViewItem user = new ListViewItem();
            user.Text = textBox1.Text;
            sender_name = textBox1.Text;
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == user.Text)
                    is_exist = true;
            }
            if(!is_exist)
                listView1.Items.Add(user);
            //addr = addrs[Array.IndexOf(nicknames, textBox1.Text)].Split(':')[0];
            SetAddress(textBox1.Text);
            //WriteMessage(ports[1]);
        }

        void copyMenuItem_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

            field.Enabled = true;
            button3.Enabled = true;
            state = true;
            bool selected = false;


            string name = "";
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    name = item.Text;
                }
              
            }
            //if (!isReadied.ContainsKey(name))
            //isReadied.Add(name, false);
            //if (File.Exists($@"{name}.log")){

            if (isReadied[name] == false)
            {

                panel1.Controls.Clear();
                msg_count = 0;
                foreach (string msg in File.ReadAllLines($@"history/{name}.log"))
                {
                    WriteMessage(msg, false, state);
                }
                isReadied[name] = true;
                foreach (var item in isReadied)
                {
                    if (item.Key != name)
                        isReadied[item.Key] = false;
                }
               
                //}
                SetAddress(name);
            }
            //addr = addrs[Array.IndexOf(nicknames, name)].Split(':')[0];
        }
    }
    
}
