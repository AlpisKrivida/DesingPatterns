using Lab1_1;
using Lab1_1.Facade;
using Lab1_1.Observer;
using Lab1_1.Streategy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.DecoratorPattern;
using WindowsFormsApp2.StatePattern;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public static int mapSize = 10;
        public static int border = 4;
        public static int titleSizeX = 100;
        public static int titleSizeY = 100;

        public Map map;
        public int offsetX = 0;
        public int offsetY = 0;

        Bitmap bm;
        Graphics gfx;

        public static HttpClient client = new HttpClient();
        public GameManager gameManager = new GameManager(client);

        private Context context;

        public Form1()
        {
            InitializeComponent();
        }

        private async Task Init()
        {
            bm = new Bitmap(mapSize * titleSizeX, mapSize * titleSizeY);
            gfx = Graphics.FromImage(bm);


            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("https://localhost:44393/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(Constants.mediaType));

            gameManager = new GameManager(client);
            gameManager.generateGrid(mapSize, mapSize);

            if (Constants.online)
            {
                List<Unit> serverMap = await gameManager.GetPlayerMap(1);
                Map.GetInstance.ConvertListToArray(serverMap);
            }

            context = new Context(gameManager);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Constants.form = this;
            await Init();
            RenderMap();
        }

        public void RenderMap()
        {
            Cell c = new Cell();
            List<TextureBrush> tb = new List<TextureBrush>();

            tb.Add((TextureBrush)new GrassTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new YellowTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new YellowDot(c).GetBrush().Clone());
            tb.Add((TextureBrush)new StoneTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new WonderTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new GoldMineTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new BlueTile(c).GetBrush().Clone());
            tb.Add((TextureBrush)new BlueDot(c).GetBrush().Clone());

            gfx.FillRectangle(Brushes.Black, 0, 0, mapSize * titleSizeX, mapSize * titleSizeY + 100);
            for (int x = 0; x < gameManager.GetYSize(); x++)
            {
                for (int y = 0; y < gameManager.GetXSize(); y++)
                {
                    TextureBrush tBrush = GetBrush(gameManager.GetColor(x, y), gameManager.GetSymbol(x, y), tb);
                    tBrush.Transform = new System.Drawing.Drawing2D.Matrix(
                        100.0f / 512.0f,
                        0.0f,
                        0.0f,
                        100.0f / 512.0f,
                        0.0f,
                        0.0f);
                    gfx.FillRectangle(tBrush, 0 + offsetX + border, 0 + offsetY + border, titleSizeX - 5, titleSizeY - 5);

                    offsetY += titleSizeY;
                }
                offsetY = 0;
                offsetX += titleSizeX;
            }
            offsetX = 0;
            pictureBox1.Image = bm;
        }

        private new void Move(string direction)
        {
            
            context.NextState(direction);
            RenderMap();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Constants.IsButtonActive)
            {
                if (e.KeyCode == Keys.A)
                {
                    Move("L");
                }
                if (e.KeyCode == Keys.D)
                {
                    Move("R");
                }
                if (e.KeyCode == Keys.W)
                {
                    Move("U");
                }
                if (e.KeyCode == Keys.S)
                {
                    Move("D");
                }
            }
        }

        private TextureBrush GetBrush(ConsoleColor n, char symbol, List<TextureBrush> tb)
        {
            switch (symbol)
            {
                case '*':
                    if (n == (ConsoleColor)14)
                    {
                        return tb[2];
                    }
                    else if (n == (ConsoleColor)9)
                    {
                        return tb[7];
                    }
                    return tb[0];
                case 'G':
                    return tb[5];
                case 'S':
                    return tb[3];
                case 'W':
                    return tb[4];
                case '0':
                    if (n == (ConsoleColor)14)
                    {
                        return tb[1];
                    }
                    else if (n == (ConsoleColor)9)
                    {
                        return tb[6];
                    }
                    else if (n == (ConsoleColor)15)
                    {
                        return tb[0];
                    }
                    return tb[0];
                default:
                    return tb[0];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            context.NextState("");
            RenderMap();
        }
    }
}
