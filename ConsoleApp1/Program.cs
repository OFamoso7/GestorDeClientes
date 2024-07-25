using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>(); 

        enum Menu { Listagem = 1, Adicionar = 2 , Remover = 3, Sair}
        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - bem vindo");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;


                }
                Console.Clear();

            }

            


        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente");
            Console.WriteLine("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido!\nAperte enter para sair");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if(clientes.Count > 0)
            {
                Console.WriteLine("!!!Lista de Clientes!!!");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail:{cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    i++;
                    Console.WriteLine("-----------------------------------");
                }
            
            }
            else
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }

            Console.WriteLine("aperte enter para sair");
            Console.ReadLine();
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat",FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();
            
            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("digite o ID do cliente a ser removido");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {

                clientes.RemoveAt(id);
                Salvar();

            }
            else
            {
                Console.WriteLine("ID inválido, tente novamente!");
                Console.ReadLine();
            }
            Console.WriteLine("ID removido!\n -- Aperte enter --");
            Console.ReadLine();

        }


        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try
            {
                
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }

                
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }


    }
}
