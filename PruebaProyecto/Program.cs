using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using PruebaProyecto.Modelos;

namespace Banco_de_Sangre
{
    class Program
    {
        public static List<ModeloSanguineo> InvSang = InciarInventario();//Lista pública para utilizar los datos de la lista 'Inventario' (Tipo de Sangre, cantidad de litros)
        public static List<SolicitudSang> SolSang = new List<SolicitudSang>();//Lista que guardará los datos de solicitante 
        static void Main()
        {
            #region Menú de Opciones
            Console.Clear();
            Int32 vOp;
            Console.WriteLine("MENÚ DE OPCIONES");
            Console.WriteLine("1. Registrar un usuario");
            Console.WriteLine("2. Inventario de sangre"); //Buscar el nombre correcto
            Console.WriteLine("3. Verificar compatibilidad de sangre");
            Console.WriteLine("9. Terminar");
            Console.WriteLine();
            Console.Write("Elija la opción que necesite escribiendo el número establecido en el menú: ");
            vOp = Convert.ToInt32(Console.ReadLine());

            switch (vOp)
            {
                case 1:
                    RegistrarUsuario();
                    break;
                case 2:
                    Inventario();
                    break;
                case 3:
                    Compatibilidad();
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Debe elegir un número que coincida con una opción del MENÚ.");
                    Console.WriteLine("Presione la tecla 'ENTER' o cualquier otra.");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                    break;
            }
            #endregion
        }

        static void Mostrar(string vPregunta) //Función para mostrar por pantalla las preguntas del formulario
        {
            Console.Write(vPregunta);
        }

        static string Pedir() //Pide la respuesta al usuario y la convierte a 'Lower' para evitar errores si se escribe en mayúscula.
        {
            string x = Console.ReadLine();
            x = x.ToLower();
            return x;
        }

        static string PedirMayus() //Pide respuesta al usuario y la convierte a 'Upper' para evitar errores si se escribe en minúscula
        {
            string y = Console.ReadLine();
            y = y.ToUpper();
            return y;
        }

        static void RegistrarUsuario()
        {
            #region Registrar un usuario
            Console.Clear();
            Int32 vOp1;
            Console.WriteLine("Menú 'Registrar un usuario'");
            Console.WriteLine("1. ¿Quiere donar?");
            Console.WriteLine("2. Solicitud de sangre");
            Console.WriteLine("3. Registrar donante");
            Console.WriteLine("4. Volver al MENÚ Principal");
            Console.WriteLine();
            Console.Write("Elija la opción que necesite escribiendo el número establecido en el menú: ");
            vOp1 = Convert.ToInt32(Console.ReadLine());

            switch (vOp1)
            {
                case 1:
                    Console.WriteLine();
                    FormularioDonante(); //aqui se incluyó el formulario
                    Console.Write("Presione 'ENTER' para volver al Menú 'Registrar un usuario'.");
                    Console.ReadKey();
                    RegistrarUsuario();
                    break;

                case 2:
                    Console.WriteLine();
                    Console.WriteLine("Favor llenar este formulario para obtener sus datos.");
                    FormularioSolicitud(); //Fromulario para guardar datos de los que necesitan sangre
                    Console.Write("Presione 'ENTER' para volver al Menú 'Registrar un usuario'.");
                    Console.ReadKey();
                    RegistrarUsuario();
                    break;

                case 3:
                    Console.WriteLine();
                    RegistrarDonante();
                    
                    break;

                case 4:
                    Main();
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("Debe elegir un número que coincida con una opción del MENÚ.");
                    Console.WriteLine("Presione la tecla 'ENTER' o cualquier otra.");
                    Console.ReadKey();
                    Console.Clear();
                    RegistrarUsuario();
                    break;
            }
            #endregion
        }

        static void FormularioDonante() //Formulario con preguntas desicivas para saber si puede donar o no
        {
            #region Formulario del Donante
            Console.Clear();
            Console.WriteLine("Favor llenar este formulario para verificar si es apto para donar. (Respuestas si/no)");
            Console.WriteLine();
            //preguntas
            Mostrar("1. ¿Tiene entre 18-65 años y pesa más de 50Kg (110 lb)? ");
            string vResp1 = Pedir();
            Mostrar("2. ¿Tiene buena salud? ");
            string vResp2 = Pedir();
            Mostrar("3. ¿Está tomando o ha tomado en los últimos días algún medicamento? ");
            string vResp3 = Pedir();
            Mostrar("4. ¿Ha presentado fiebre, acompañada de dolor de cabeza y malestar general? ");
            string vResp4 = Pedir();
            Mostrar("5. ¿Se inyecto o se inyecta drogas intravenosas ilegales o aspira cocaína regularmente? ");
            string vResp5 = Pedir();
            Mostrar("6. ¿Padece diabetes tratada con insulina de origen animal? ");
            string vResp6 = Pedir();
            Mostrar("7. ¿Ha padecido hepatitis, ictericia o problemas de hígado ? ");
            string vResp7 = Pedir();
            Mostrar("8. ¿Ha padecido algún tipo de cáncer? ");
            string vResp8 = Pedir();
            Mostrar("9. ¿Ha recibido alguna vacuna diferente de vacunas antialérgicas, gripe, vacuna para la hepatitis A o vacuna contra el tétano? ");//
            string vResp9 = Pedir();
            Mostrar("10. ¿Se realizó tatuajes, colocación de piercing o acupuntura en los últimos 6 meses? ");
            string vResp10 = Pedir();

            String[] Si = { vResp1, vResp2 };
            String[] No = { vResp3, vResp4, vResp5, vResp6, vResp7, vResp8, vResp9, vResp10 };
            bool RespFinal = true, RespFinal2 = true;

            //Foreach que verifica que todas las respuestas que deben ser "si", lo son
            #region Si
            foreach (string element in Si)
            {
                if (element != "si") //si la respuesta es "no", la variable es 'false'
                {
                    RespFinal = false;
                }
            }
            #endregion

            //Foreach que verifica que todas las respuestas que deben ser "no", lo son
            #region No
            foreach (string element in No)
            {
                if (element != "no") //si la respuesta es "si", la variable es 'false'
                {
                    RespFinal2 = false;
                }
            }
            #endregion

            //If, Then, Else que comprueba si el usuario puede donar o no
            if (RespFinal == true && RespFinal2 == true)
            {
                Console.WriteLine("Puede donar");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No puede donar");
                Console.WriteLine();
            }
            #endregion
        }

        static void FormularioSolicitud() //Formulario para solicitar sangre
        {
            #region Formulario Solicitante
            Console.Clear();
            string vResp, vResp2;
            SolicitudSang ss = new SolicitudSang(); //Registro que se agregará a la lista 'SolSang'

            Console.WriteLine("INDENTIFICACIÓN DEL PACIENTE");
            Console.Write("Nombre: ");
            ss.vNombre = Console.ReadLine();
            Console.Write("Apellido: ");
            ss.vApellido = Console.ReadLine();
            Console.Write("Edad: ");
            ss.vEdad = Convert.ToInt32(Console.ReadLine());
            Console.Write("Cédula: ");
            ss.vCedula = Console.ReadLine();
            Console.Write("Indique el tipo de sangre solicitado: ");
            ss.vTipoSanguineo = PedirMayus();

            foreach (var item in InvSang)
            {
                if (item.TipoSanguineo == ss.vTipoSanguineo)
                {
                    Console.WriteLine("Tenemos disponibles: " + item.Inventario + " litros");
                }
            }

            Console.Write("Cantidad requerida de litros: ");
            ss.vCant = Convert.ToDouble(Console.ReadLine());

            Console.Write("¿Retira esta cantidad? (si/no): ");
            vResp = Pedir();

            if (vResp == "si")
            {
                foreach (var item in InvSang)
                {
                    if (item.TipoSanguineo == ss.vTipoSanguineo && item.Inventario >= ss.vCant)
                    {
                        item.Inventario = item.Inventario - ss.vCant;
                        Console.WriteLine();
                        Console.WriteLine("Su Solicitud fue recibida con éxito.");
                    }
                    else
                    {
                        if (item.TipoSanguineo == ss.vTipoSanguineo && item.Inventario < ss.vCant)
                        {
                            Console.WriteLine("No tenemos disponible la cantidad total de litros requerida del componente " + ss.vTipoSanguineo);
                            Console.Write("¿Desea retirar la cantidad existente de litros y ser añadido a la lista de solicitudes? (si/no): ");
                            vResp2 = Pedir();
                            if (vResp2 == "si")
                            {
                                SolSang.Add(ss); //agregar el registro 'ss' a la lista 'SolSang'
                                ss.vCant = ss.vCant - item.Inventario;
                                item.Inventario = 0;
                                Console.WriteLine();
                                Console.WriteLine("Fue agregado a la lista de solicitudes en el Inventario.");
                            }
                            else
                            {
                                Console.Write("¿Desea ser agregado a la lista de solicitudes con su requerimiento completo? (si/no): ");
                                vResp2 = Pedir();
                                if (vResp2 == "si")
                                {
                                    SolSang.Add(ss);
                                    Console.WriteLine();
                                    Console.WriteLine("Fue agregado a la lista de solicitudes en el Inventario.");
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine();
            #endregion
        }

        static void RegistrarDonante()
        {
            #region Registrar un donante
            Console.Clear();
            SolicitudSang Donante = new SolicitudSang();
            string vUsuario, vContraseña;
            Console.Write("Usuario: ");
            vUsuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            vContraseña = Console.ReadLine();

            if (vUsuario == "Grupo4" && vContraseña == "Pass1234")
            {
                Console.Clear();
                Console.WriteLine("IDENTIFICAIÓN DEL DONANTE");
                Console.Write("Nombre: ");
                Donante.vNombre = Console.ReadLine();
                Console.Write("Apellido: ");
                Donante.vApellido = Console.ReadLine();
                Console.Write("Edad: ");
                Donante.vEdad = Convert.ToInt32(Console.ReadLine());
                Console.Write("Cédula: ");
                Donante.vCedula = Console.ReadLine();
                Console.Write("Tipo Sanguíneo: ");
                Donante.vTipoSanguineo = PedirMayus();
                Console.Write("Cantidad donada de litros: ");
                Donante.vCant = Convert.ToDouble(Console.ReadLine());

                foreach (var item in InvSang)
                {
                    if (item.TipoSanguineo == Donante.vTipoSanguineo)
                    {
                        item.Inventario = item.Inventario + Donante.vCant;
                    }
                }

                Console.WriteLine("La donación de " + Donante.vNombre + " " + Donante.vApellido + ", ha sido registrada con éxito.");
                Console.WriteLine();
                Console.Write("Presione 'ENTER' para volver al Menú 'Registrar un usuario'.");
                Console.ReadKey();
                RegistrarUsuario();
            }
            else
            {
                Console.WriteLine("Contraseña o Usurio incorreto.");
                Console.WriteLine("Presione 'ENTER' para volver al Menú Principal");
                Console.ReadKey();
                Main();
            }
            #endregion
        }

        static void Inventario()
        {
            #region Inventario de sangre
            //Todo esto escribirlo más formal
            Console.Clear();
            Console.WriteLine("Tenemos estos litros disponibles: ");
            Console.WriteLine();
            foreach (var item in InvSang)
            {
                Console.WriteLine(item.TipoSanguineo + ": " + item.Inventario + " litros");
            }
            Console.WriteLine();
            Console.WriteLine("Solicitudes pendientes: ");
            Console.WriteLine();
            foreach (var item in SolSang)
            {
                Console.WriteLine(item.vTipoSanguineo + ", " + item.vCant + " litros: " + item.vNombre + " " + item.vApellido);
            }
            Console.WriteLine();
            Console.Write("Presione 'ENTER' para volver al MENÚ Principal");
            Console.ReadKey();
            Main();
            #endregion
        }

        static void Compatibilidad()
        {
            #region Compatibilidad
            Console.Clear();
            Int32 vOp2;
            Console.WriteLine("Menú 'Compatibilidad de sangre'");
            Console.WriteLine("1. Verificar Compatibilidad Sanguínea");
            Console.WriteLine("2. Volver al MENÚ Principal");
            Console.WriteLine();
            Console.Write("Elija la opción que necesite escribiendo el número establecido en el menú: ");
            vOp2 = Convert.ToInt32(Console.ReadLine());
            switch (vOp2)
            {
                case 1:
                    CompSangre();
                    break;

                case 2:
                    Main();
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("Debe elegir un número que coincida con una opción del MENÚ.");
                    Console.WriteLine("Presione la tecla 'ENTER' o cualquier otra.");
                    Console.ReadKey();
                    Console.Clear();
                    Compatibilidad();
                    break;
            }
            #endregion
        }

        static void CompSangre()
        {
            #region Compatibilidad sanguínea
            Console.Clear();
            String vSangre;
            Console.Write("Indique el tipo de sangre: ");
            vSangre = Console.ReadLine();
            vSangre = vSangre.ToUpper();
            //String vSangre2 = vSangre.ToUpper();
            //vSangre = vSangre2;

            switch (vSangre)
            {
                case "A+":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: A+, AB+");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: O+, O-, A+, A-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "A-":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: A+, A-, AB+, AB-");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: O-, A-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "B+":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: B+, AB+");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: O+, O-, B+, B-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "B-":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: B+, B-, AB+, AB-");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: O-, B-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "AB+":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: AB+");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: TODOS");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "AB-":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: AB+, AB-");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: AB-, O-, A-, B-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "O+":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: A+, B+, AB+, O+");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: O+, O-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                case "O-":
                    Console.WriteLine();
                    Console.WriteLine("Puede donar a: TODOS");
                    Console.WriteLine();
                    Console.WriteLine("Puede recibir de: O-");
                    Console.WriteLine();
                    Console.Write("Presione 'ENTER' para volver al Menú 'Compatibilidad de sangre'");
                    Console.ReadKey();
                    Compatibilidad();
                    break;

                default:
                    Console.WriteLine();
                    Console.WriteLine("El tipo de Sangre ingresado no existe o ha ignorado el factor RH (positivo o negativo)");
                    Console.WriteLine();
                    Console.WriteLine("Presione 'ENTER' para ingresar otro tipo de sangre");
                    Console.ReadKey();
                    CompSangre();
                    break;
            }
            #endregion
        }

        static List<ModeloSanguineo> InciarInventario()
        {
            #region Modelo
            List<ModeloSanguineo> Inventario = new List<ModeloSanguineo>(); //Creación de lista donde se guardarán los datos del Inventario

            ModeloSanguineo Ap = new ModeloSanguineo
            {
                ID = 1, TipoSanguineo = "A+", Inventario = 40
            };
            Inventario.Add(Ap);

            ModeloSanguineo An = new ModeloSanguineo
            {
                ID = 2, TipoSanguineo = "A-", Inventario = 37
            };
            Inventario.Add(An);

            ModeloSanguineo Bp = new ModeloSanguineo
            {
                ID = 3, TipoSanguineo = "B+", Inventario = 45
            };
            Inventario.Add(Bp);

            ModeloSanguineo Bn = new ModeloSanguineo
            {
                ID = 4, TipoSanguineo = "B-", Inventario = 43
            };
            Inventario.Add(Bn);

            ModeloSanguineo ABp = new ModeloSanguineo
            {
                ID = 5, TipoSanguineo = "AB+", Inventario = 34
            };
            Inventario.Add(ABp);

            ModeloSanguineo ABn = new ModeloSanguineo
            {
                ID = 6, TipoSanguineo = "AB-", Inventario = 39
            };
            Inventario.Add(ABn);

            ModeloSanguineo Op = new ModeloSanguineo
            {
                ID = 7, TipoSanguineo = "O+", Inventario = 46
            };
            Inventario.Add(Op);

            ModeloSanguineo On = new ModeloSanguineo
            {
                ID = 8, TipoSanguineo = "O-", Inventario = 48
            };
            Inventario.Add(On);
            return Inventario;
            #endregion
        }
    }
}
