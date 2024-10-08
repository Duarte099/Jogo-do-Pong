using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace JogoPong
{
    // A classe Program herda da classe GameWindow do OpenTK, que é uma janela para fazer aplicações gráficas
    class Program : GameWindow
    {
        // Variáveis para a posição e tamanho da bola
        int xDaBola = 0;
        static int yDaBola = 0;
        int tamanhoDaBola = 20;

        // Velocidades iniciais da bola nas direções X e Y
        static int velocidadeDaBolaEmX = 3;
        static int velocidadeDaBolaEmY = 3;

        // Pontuações dos jogadores
        int ScoreP1 = 0;
        int ScoreP2 = 0;

        // Posições verticais dos jogadores 1 e 2
        static int[] yDoJogador = new int[2];

        // Modo de jogo SinglePlayer, MultiPlayer ou Computador
        static string modoJogo = "";

        static int vezJogador = 1;

        static float sensibilidade = 0.01f;

        // Função que faz uma contagem regressiva de 1 a 5
        static void ContagemRegressiva()
        {
            for (int i = 5; i > 0; i--)
            {
                Console.Write($"{i}...");
                System.Threading.Thread.Sleep(1000);
            }
        }

        // Função que exibe o menu e retorna a escolha da pessoa
        static ConsoleKeyInfo Menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Jogar               ");
            Console.WriteLine("2 - Como Jogar          ");
            Console.WriteLine("3 - Alterar dificuldade ");
            ConsoleKeyInfo escolha = Console.ReadKey();
            return escolha;
        }

        // Função que retorna a posição horizontal inicial do jogador 1
        int xDoJogador1()
        {
            return -ClientSize.Width / 2 + larguraDosJogadores() / 2;
        }

        // Função que retorna a posição horizontal inicial do jogador 2
        int xDoJogador2()
        {
            return ClientSize.Width / 2 - larguraDosJogadores() / 2;
        }

        // Função que retorna a largura dos jogadores
        int larguraDosJogadores()
        {
            return tamanhoDaBola / 2;
        }

        // Função que retorna a altura dos jogadores
        int alturaDosJogadores()
        {
            return 4 * tamanhoDaBola;
        }

        // Método do OpenTK chamado a cada frame para atualizar a lógica do jogo
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Atualiza a posição da bola com base nas velocidades
            xDaBola = xDaBola + velocidadeDaBolaEmX;
            yDaBola = yDaBola + velocidadeDaBolaEmY;

            // Verifica colisões com as plataformas dos jogadores
            if (xDaBola + tamanhoDaBola / 2 > xDoJogador2() - larguraDosJogadores() / 2
             && yDaBola - tamanhoDaBola / 2 < yDoJogador[1] + alturaDosJogadores() / 2
             && yDaBola + tamanhoDaBola / 2 > yDoJogador[1] - alturaDosJogadores() / 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
                vezJogador = 0;
            }

            if (xDaBola - tamanhoDaBola / 2 < xDoJogador1() + larguraDosJogadores() / 2
             && yDaBola - tamanhoDaBola / 2 < yDoJogador[0] + alturaDosJogadores() / 2
             && yDaBola + tamanhoDaBola / 2 > yDoJogador[0] - alturaDosJogadores() / 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
                vezJogador = 1;
            }

            // Verifica colisões com as bordas superiores e inferiores da tela
            if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
            }

            if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
            }

            // Verifica se a bola ultrapassou as bordas laterais da tela se sim reseta a posição da bola e atualiza o score dependendo do lado onde entrou
            if (xDaBola < -ClientSize.Width / 2)
            {
                ScoreP2 = ScoreP2 + larguraDosJogadores();
                xDaBola = 0;
                yDaBola = 0;
            }
            else if (xDaBola > ClientSize.Width / 2)
            {
                ScoreP1 = ScoreP1 + larguraDosJogadores();
                xDaBola = 0;
                yDaBola = 0;
            }

            // Verifica se os jogadores atingiram as bordas superior e inferior da tela
            if (yDoJogador[0] + alturaDosJogadores() / 2 > ClientSize.Height / 2)
            {
                yDoJogador[0] = yDoJogador[0] - 5;
            }

            if (yDoJogador[0] - alturaDosJogadores() / 2 < -ClientSize.Height / 2)
            {
                yDoJogador[0] = yDoJogador[0] + 5;
            }

            if (yDoJogador[1] + alturaDosJogadores() / 2 > ClientSize.Height / 2)
            {
                yDoJogador[1] = yDoJogador[1] - 5;
            }

            if (yDoJogador[1] - alturaDosJogadores() / 2 < -ClientSize.Height / 2)
            {
                yDoJogador[1] = yDoJogador[1] + 5;
            }
            // Movimentação dos jogadores com as teclas W, S, tecla para cima e para baixo, cada 2 para cada player
            switch (modoJogo)
            {
                case "SinglePlayer":
                    if (vezJogador == 0)
                    {
                        if (Keyboard.GetState().IsKeyDown(Key.Up) || Keyboard.GetState().IsKeyDown(Key.W))
                        {
                            yDoJogador[0] = yDoJogador[0] + 5;
                        }

                        if (Keyboard.GetState().IsKeyDown(Key.Down) || Keyboard.GetState().IsKeyDown(Key.S))
                        {
                            yDoJogador[0] = yDoJogador[0] - 5;
                        }
                    }
                    else if (vezJogador == 1)
                    {
                        if (Keyboard.GetState().IsKeyDown(Key.Up) || Keyboard.GetState().IsKeyDown(Key.W))
                        {
                            yDoJogador[1] = yDoJogador[1] + 5;
                        }

                        if (Keyboard.GetState().IsKeyDown(Key.Down) || Keyboard.GetState().IsKeyDown(Key.S))
                        {
                            yDoJogador[1] = yDoJogador[1] - 5;
                        }
                    }
                    break;

                case "MultiPlayer":
                    if (Keyboard.GetState().IsKeyDown(Key.W))
                    {
                        yDoJogador[0] = yDoJogador[0] + 5;
                    }

                    if (Keyboard.GetState().IsKeyDown(Key.S))
                    {
                        yDoJogador[0] = yDoJogador[0] - 5;
                    }

                    if (Keyboard.GetState().IsKeyDown(Key.Up))
                    {
                        yDoJogador[1] = yDoJogador[1] + 5;
                    }

                    if (Keyboard.GetState().IsKeyDown(Key.Down))
                    {
                        yDoJogador[1] = yDoJogador[1] - 5;
                    }
                    break;

                case "Computador":
                    if (Keyboard.GetState().IsKeyDown(Key.Up) || Keyboard.GetState().IsKeyDown(Key.W))
                    {
                        yDoJogador[0] = yDoJogador[0] + 5;
                    }

                    if (Keyboard.GetState().IsKeyDown(Key.Down) || Keyboard.GetState().IsKeyDown(Key.S))
                    {
                        yDoJogador[0] = yDoJogador[0] - 5;
                    }

                    float diferenca = yDaBola - yDoJogador[1];
                    yDoJogador[1] = yDoJogador[1] + (int)(diferenca * sensibilidade);
                    break;
            }
        }

        // Método do OpenTK chamado a cada frame para renderizar
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Define a área de renderização
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            // Cria uma matriz de ortográfica
            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            // Limpa o buffer de cores
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Desenha a bola e os jogadores
            DesenharBola(xDaBola, yDaBola, tamanhoDaBola / 2, 1.0f, 1.0f, 0.0f);
            DesenharRetangulo(xDoJogador1(), yDoJogador[0], larguraDosJogadores(), alturaDosJogadores(), 1.0f, 0.0f, 0.0f);
            DesenharRetangulo(xDoJogador2(), yDoJogador[1], larguraDosJogadores(), alturaDosJogadores(), 0.0f, 0.0f, 1.0f);

            // Desenha as barras de pontuação
            DesenharRetangulo(-ClientSize.Width / 2, (ClientSize.Height / 2) - 2, ScoreP1, 8, 1.0F, 0.0F, 0.0F);
            DesenharRetangulo(ClientSize.Width / 2, (ClientSize.Height / 2) - 2, ScoreP2, 8, 0.0F, 0.0F, 1.0F);

            // Troca os buffers para exibir a cena renderizada
            SwapBuffers();
        }

        // Função que desenha a bola como um polígono (círculo)
        void DesenharBola(int x, int y, int raio, float r, float g, float b)
        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Polygon);
            for (int i = 0; i < 360; i++)
            {
                float grausParaRadianos = i * 3.1416f / 180;
                GL.Vertex2(x + Math.Cos(grausParaRadianos) * raio, y + Math.Sin(grausParaRadianos) * raio);
            }
            GL.End();
        }

        // Função que desenha um retângulo (usado para os jogadores e as barras de pontuação)
        void DesenharRetangulo(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }

        // Função principal do jogo
        static void jogoMain()
        {
            // Exibe o menu e obtém a escolha do jogador
            ConsoleKeyInfo escolha = Menu();

            switch (escolha.Key)
            {
                // Se o usuário escolher a opção jogar, então passa para a tela de escolehr o modo de jogo para depois inica-lo
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.WriteLine("\nEscolha o modo de jogo");
                    Console.WriteLine("1 - SinglePlayer             ");
                    Console.WriteLine("2 - MultiPlayer              ");
                    Console.WriteLine("3 - Jogar contra computador  ");
                    Console.WriteLine("4 - Voltar ao menu           ");

                    ConsoleKeyInfo escolhaJogo = Console.ReadKey();
                    switch (escolhaJogo.Key)
                    {
                        // Subcaso 1.1: SinglePlayer
                        case ConsoleKey.D1:
                            modoJogo = "SinglePlayer";
                            break;

                        // Subcaso 1.2: MultiPlayer
                        case ConsoleKey.D2:
                            modoJogo = "MultiPlayer";
                            break;

                        // Subcaso 1.3: Jogar contra computador
                        case ConsoleKey.D3:
                            modoJogo = "Computador";
                            break;

                        case ConsoleKey.D4:
                            jogoMain();
                            break;

                        // Subcaso padrão: Opção inválida
                        default:
                            Console.WriteLine("\nOpção inválida. Por favor, escolha uma opção válida.");
                            Console.Write("Retornando para o menu ");
                            ContagemRegressiva();
                            jogoMain();
                            break;
                    }
                    Console.Write("\nCarregando modo de jogo ");
                    ContagemRegressiva();
                    new Program().Run();
                    break;

                // Se o usuário escolher a opção como jogar, então o programa indica ao utilizador como jogar
                case ConsoleKey.D2:
                    Console.WriteLine("\nInstruções de como jogar");
                    Console.WriteLine("\nJogo Pong, um jogo muito conhecido, tradicionalmente por \"Ping Pong\" o objetivo do jogo é mover as plataformas para que a bola faça ricochete nela e avançe para o outro jogador fazendo assim com que não embata contra as bordas.");
                    Console.WriteLine("Modo SinglePlayer: É o mesmo jogo mas so com 1 player, ele controla as duas plataformas, com o W, S ou com as setas");
                    Console.WriteLine("Modo multiPlayer: Cada player controla a sua plataforma, uma com o W e S e a outra com as setas");
                    Console.WriteLine("Modo com computador: É como se fosse o SinglePlayer mas o segundo player é o computador ");
                    Console.WriteLine("Precione uma tecla para continuar...");
                    Console.ReadKey();
                    jogoMain();
                    break;

                // Se o usuário escolher trocar a dificuldade, então passa para a tela de trocar a dificuldade 
                case ConsoleKey.D3:
                    Console.Clear();
                    Console.WriteLine("\nAlterar a dificuldade");
                    Console.WriteLine("1 - EASY               ");
                    Console.WriteLine("2 - MEDIUM             ");
                    Console.WriteLine("3 - HARD               ");
                    Console.WriteLine("4 - Voltar ao menu     ");
                    ConsoleKeyInfo escolhaDificuldade = Console.ReadKey();
                    switch (escolhaDificuldade.Key)
                    {
                        case ConsoleKey.D1:
                            velocidadeDaBolaEmX = 3;
                            velocidadeDaBolaEmY = 3;
                            sensibilidade = 0.008f;
                            break;

                        case ConsoleKey.D2:
                            velocidadeDaBolaEmX = 5;
                            velocidadeDaBolaEmY = 5;
                            sensibilidade = 0.009f;
                            break;

                        case ConsoleKey.D3:
                            velocidadeDaBolaEmX = 7;
                            velocidadeDaBolaEmY = 7;
                            sensibilidade = 0.01f;
                            break;

                        case ConsoleKey.D4:
                            jogoMain();
                            break;

                        default:
                            // Se a escolha for inválida, mostra uma mensagem e retorna ao menu principal.
                            Console.WriteLine("\nOpção inválida. Por favor, escolha uma opção válida.");
                            Console.Write("Retornando para o menu ");
                            ContagemRegressiva();
                            jogoMain();
                            break;
                    }
                    Console.Write("\nAlterando dificuldade ");
                    ContagemRegressiva();
                    jogoMain();
                    break;

                // Se o usuário escolher trocar o tema, então passa para a tela de trocar o tema 
                case ConsoleKey.D4:
                    Console.Write("\nAlterando tema ");
                    ContagemRegressiva();
                    break;

                // Se a escolha for inválida, mostra uma mensagem e retorna ao menu principal.
                default:
                    Console.WriteLine("\nOpção inválida. Por favor, escolha uma opção válida.");
                    Console.Write("Retornando para o menu ");
                    ContagemRegressiva();
                    jogoMain();
                    break;
            }
        }

        // Função principal do programa.
        static void Main()
        {
            jogoMain();
        }
    }
}
