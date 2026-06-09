using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Diagnostics;

namespace Recepcion_Bascula
{
    public partial class Recepcion_bascula : Form
    {
        MySqlConnection mySqlConn = new MySqlConnection("server=gab.mrlucky.com.mx;userid=www1166;password=taQ17Zm;database=campo");
        MySqlCommand cmnd = new MySqlCommand();
        MySqlCommand cmnd0 = new MySqlCommand();
        MySqlDataReader reader;

        public static SqlConnection thisConnection = new SqlConnection(Utilerias.Class1.ConnectionString);
        SqlCommand cmnd1 = new SqlCommand();
        SqlCommand cmnd11 = new SqlCommand();
        SqlDataReader reader1, reader11;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        DataTable fletes = new DataTable();

        public static string lin, prod, numflete = "", id_proveedor, id_rancho, origen, id_responsable, plataforma, numero_economico, chofer, tipo_transporte, id_linea, tipo, transportista, rancho, proveedor, id_unidad, id_variedad, vehiculo = "", productos = "", unities = "", clasificacion = "", estatus, clave_bas, hay, detalle, envase = "", rec_esp = "", tabl = "";
        private string aux_id_rancho, aux_observacion, aux_num_economico, aux_od, prov_clave = "", rch_clave, tbl_clave, tbl_nombre;
        private string aux_transporte, aux_chofer, aux_responsable, aux_id_tipo_flete, aux_id_responsable, aux_origen;
        private string aux_valor_operador, aux_valor_transportista, consulta = "N";
        private string aux_id_proveedor, aux_nombre_proveedor, aux_opcion = "", aux_presentacion, texto, tant, aux_nom_variedad, fecha_carga;
        private Int32 lastId, numero, numero_renglones, rencontrol, coor_y_ima, bas_clave, opcion;
        public static double peso_entrada, peso_bruto, tara, peso_neto;
        public static int id_ticket, suma_prod = 0, cont = 0;
        public static DateTime fecha;
        List<string> variedad = new List<string>();
        List<string> unidades = new List<string>();
        List<string> clave_unidades = new List<string>();
        public static string nombre_unidad = "";
        public static decimal canti = 0;
        public static string esttus = "";

        TextBox textBox10 = new TextBox();
        ComboBox envases = new System.Windows.Forms.ComboBox();
        DataTable ranchos = new DataTable();
        DataTable tablas = new DataTable();
        DataTable TickPen = new DataTable();

        public string _Speed;
        public string _GetBitSerial;
        public string _GetBitSerialFull;

        private Font fuente = new Font("Arial", 8);

        public Recepcion_bascula()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
        }

        //public void tabla()
        //{
        //    fletes.Columns.Add("folio");
        //    fletes.Columns.Add("proveedor");
        //    fletes.Columns.Add("orde");
        //    fletes.Columns.Add("id_destino");
        //}

        //cargar fletes pendientes

        private void cargar_fletes_pendientes_internetLEGACY()
        {
            listBox1.Items.Clear();
            MySqlCommand cmnd1 = mySqlConn.CreateCommand();
            MySqlDataReader reader;
            //cmnd1.CommandText = "SELECT tb_mstr_flete.id_flete, tb_mstr_flete.nom_proveedor, tb_cat_origen_destino.orde, tb_mstr_flete.id_destino FROM tb_mstr_flete INNER JOIN tb_cat_origen_destino ON tb_mstr_flete.origen = tb_cat_origen_destino.id_od WHERE tb_mstr_flete.estatus = 'A' ORDER BY tb_mstr_flete.id_flete DESC limit 200";
            cmnd1.CommandText = "select A.id_flete, A.nom_proveedor, B.orde, A.id_destino from tb_mstr_flete A, tb_cat_origen_destino B where A.estatus = 'A' and A.origen = B.id_od and B.orde like '%COMER%'  order by A.id_flete desc";
            try
            {

                mySqlConn.Open();
                reader = cmnd1.ExecuteReader();
                while (reader.Read())
                {
                    //DataRow row = fletes.NewRow();
                    //row[0] = reader.GetString(0);
                    //row[1] = reader.GetString(1);
                    //row[2] = reader.GetString(2);
                    //row[3] = reader.GetString(3);
                    //fletes.Rows.Add(row);
                    listBox1.Items.Add(reader.GetString(0));
                    listBox1.Items.Add(reader.GetString(2));
                    listBox1.Items.Add("                                                   ");
                }
                cmnd1.Dispose();
                reader.Dispose();
                mySqlConn.Close();
            }
            catch
            {
                mySqlConn.Close();
                Console.WriteLine("Error Conectando al sitio Fletes Mrlucky\n");
            }
        }

        private void cargar_fletes_pendientes_internet()
        {
            listBox1.Items.Clear();

            // Asegurar que la conexión esté cerrada antes de abrirla
            if (mySqlConn.State == System.Data.ConnectionState.Open)
                mySqlConn.Close();

            try
            {
                mySqlConn.Open();
                using (MySqlCommand cmnd1 = mySqlConn.CreateCommand())
                {
                    cmnd1.CommandText = @"select A.id_flete, A.nom_proveedor, B.orde, A.id_destino 
                                  from tb_mstr_flete A
                                  inner join tb_cat_origen_destino B on A.origen = B.id_od 
                                  where A.estatus = 'A' 
                                    and B.orde like '%COMER%'  
                                  order by A.id_flete desc
                                  limit 200";

                    using (MySqlDataReader reader = cmnd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Evitar error si algún campo viene NULL
                            string idFlete = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            string orde = reader.IsDBNull(2) ? "" : reader.GetString(2);

                            listBox1.Items.Add(idFlete);
                            listBox1.Items.Add(orde);
                            listBox1.Items.Add("                                                   ");
                        }
                    } // reader se cierra y dispone automáticamente
                } // cmnd1 se dispone automáticamente
            }
            catch (Exception ex)
            {
                // Informar al usuario en lugar de ocultar el error
                MessageBox.Show("Error al cargar fletes pendientes: " + ex.Message,
                                "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Garantizar cierre de conexión
                if (mySqlConn.State == System.Data.ConnectionState.Open)
                    mySqlConn.Close();
            }
        }

        //carga fletes que llegan a vigilancia
        private void cargar_fletes_pendientes_vigilancia()
        {
            try
            {
                thisConnection.Open();
            }
            catch
            {
                thisConnection.Close();
                thisConnection.Open();
            }
            SqlDataAdapter listadoBascula = new SqlDataAdapter("SELECT id_entrada, (CAST(id_entrada AS varchar(8)) +  '   ->  ' + RTRIM(Nombre) + '   Empresa:  ' + RTRIM(empresa) +  '  Gafete:  ' + CAST(gafete AS varchar(8))) as nomvig  FROM tb_mstr_registros_vigilancia WHERE filtro = 'B' AND registro_bascula is null and fecha_salida is null ORDER BY id_entrada ASC", thisConnection);

            DataSet ds1 = new DataSet();
            listadoBascula.Fill(ds1, "Bascula");

            CBRegVig.DataSource = ds1.Tables[0];
            CBRegVig.DisplayMember = "nomvig".Trim();
            CBRegVig.ValueMember = "id_entrada".Trim();
            CBRegVig.SelectedIndex = -1;

            thisConnection.Close();
        }

        //habilitar o deshabilitar controles
        private void habilitar_controles(bool habilitar)
        {
            listBox1.Enabled = habilitar;
            CBRegVig.Enabled = habilitar;
            DTPFecha.Enabled = habilitar;
            txtpesoent.Enabled = habilitar;
            txttara.Enabled = habilitar;
            txttrans.Enabled = habilitar;
            txtrch.Enabled = habilitar;
            GBVeh.Enabled = habilitar;
            txtpesosal.Enabled = habilitar;
            txtpesoneto.Enabled = habilitar;
            GBTipo.Enabled = habilitar;
            txtregent.Enabled = habilitar;
            txtlin_clave.Enabled = habilitar;
            CBlinea.Enabled = habilitar;
            txtpesobruto.Enabled = habilitar;
            txtobs.Enabled = habilitar;
            txtprod_clave.Enabled = habilitar;
            CBProducto.Enabled = habilitar;
            CBRecBas.Enabled = habilitar;
        }

        //cargar linea de productos
        private void cargar_linea()
        {
            try
            {
                thisConnection.Open();

                //carga la linea
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select lin_nombre from tb_cat_linea order by lin_NOMBRE";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    CBlinea.Items.Add(reader1.GetValue(0).ToString().Trim());
                }
                reader1.Close();
                thisConnection.Close();
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //botón salir
        private void btnsalir_Click(object sender, EventArgs e)
        {
            try
            {
                thisConnection.Open();
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "SELECT TOP 1 inicio_sesion, usu_login FROM tb_cat_historial_dia where nombre_maquina = '" + Environment.MachineName + "' " +
                                    " AND sistema = 'SIPGAB' ORDER BY inicio_sesion desc";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    Utilerias.Class1.Inicio_sesion = reader1.GetSqlDateTime(0).Value;
                    Utilerias.Class1.Usu_login = reader1.GetSqlString(1).ToString();
                    Utilerias.Class1.Nombre_equipo = Environment.MachineName;
                }
                reader1.Close();

                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "update tb_cat_historial_dia set formulario = ' ' where nombre_maquina ='" + Utilerias.Class1.Nombre_equipo + "' and " +
                                    "usu_login = '" + Utilerias.Class1.Usu_login + "' and inicio_sesion = '" + Utilerias.Class1.Inicio_sesion.ToString("s") + "' " +
                                    "and sistema = 'SIPGAB'";
                reader1 = cmnd1.ExecuteReader();
                reader1.Close();
                thisConnection.Close();
                Application.Exit();
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString());
            }
            //PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            //pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 307, 393);
            //pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            //PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
            //VistaPrevia.Document = pd;
            //VistaPrevia.ShowDialog();                
        }

        private void Recepcion_bascula_Load(object sender, EventArgs e)
        {
            cargar_linea();
            //tabla();
            try
            {
                thisConnection.Open();
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select bas_nombre from tb_cat_basculeros where estatus = 'T' order by bas_clave";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    CBRecBas.Items.Add(reader1.GetValue(0).ToString().Trim());
                }
                reader1.Dispose();

                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "SELECT prov_nombre FROM tb_cat_proveedor where prov_estatus != 'B'  ORDER BY prov_nombre";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    CBProv.Items.Add(reader1.GetValue(0).ToString());
                }
                reader1.Read();

                string query = "select prov_clave, rch_clave, rch_nombre from tb_cat_ranchos order by rch_nombre";
                da = new SqlDataAdapter(query, thisConnection);
                da.Fill(ds, "ranc");
                ranchos = ds.Tables["ranc"];

                query = "select prov_clave, rch_clave, tbl_clave, tbl_nombre from tb_cat_tablas order by tbl_nombre";
                da = new SqlDataAdapter(query, thisConnection);
                da.Fill(ds, "tbla");
                tablas = ds.Tables["tbla"];
                thisConnection.Close();
                TicketsPendientes();
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // se trae los tickets que estan pendientes de Capturar el Peso y Tara para que no se les olvide registrarlos
        // se considero para esta validacion los que traen PESO y Tara en 1 RCC 12 Ago 2024 
        private void TicketsPendientes()
        {
            thisConnection.Open();
            //cmnd1.CommandText = "SELECT A.id_ticket, B.prod_nombre, A.prod_clave, A.variedad, A.cantidad, C.hora_peso, A.num_prod FROM tb_det_recepcion_bascula A, tb_cat_producto B, tb_mstr_recepcion_bascula C WHERE A.tipo_prod = 'MP' AND " +
            //                    "A.lin_clave <> '08' AND A.estatus = 'P' AND B.prod_clave = a.prod_clave AND A.prod_clave <> '' and C.id_ticket = A.id_ticket ORDER BY A.id_ticket DESC";
            string Cadena = "SELECT A.id_ticket, C.hora_peso, D.prov_nombre  " +
                            "FROM tb_det_recepcion_bascula A, tb_cat_producto B, tb_mstr_recepcion_bascula C, tb_cat_proveedor D " +
                            "WHERE A.tipo_prod = 'MP' AND A.lin_clave <> '08' AND A.estatus = 'P' AND B.prod_clave = a.prod_clave AND A.prod_clave <> '' " +
                            "and C.id_ticket = A.id_ticket AND (A.tara = 1 OR A.peso_bruto = 1) AND C.prov_clave = d.prov_clave ORDER BY A.id_ticket DESC ";
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "Ticktes"); //TickPen
            thisConnection.Close();
            DataTable Tickets = ds.Tables["Ticktes"];
            TickPen = Tickets.Clone();
            CmbTktPen.Items.Clear();
            string Tick = "";
            foreach (DataRow row in Tickets.Rows)
            {
                if (Tick != row["id_ticket"].ToString().Trim())
                {
                    CmbTktPen.Items.Add(row["id_ticket"].ToString() + " -> " + row["hora_peso"].ToString() + " -> " + row["prov_nombre"].ToString());
                    Tick = row["id_ticket"].ToString().Trim();
                }
                //cbticket.Items.Add(reader1.GetValue(0).ToString().Trim() + " -> " + reader1.GetValue(1).ToString().Trim() + " -> " + reader1.GetValue(4).ToString().Trim() + " -> " + reader1.GetValue(5).ToString().Trim());
                //ticket.Items.Add(reader1.GetValue(0).ToString().Trim());
                //prod_clave.Items.Add(reader1.GetValue(2).ToString().Trim());
                //variedad.Items.Add(reader1.GetValue(3).ToString().Trim());
                //prod_num.Items.Add(reader1.GetValue(6).ToString().Trim());
            }
            //CmbTktPen.DisplayMember = ""

        }
        //botón de alta
        private void btnAlta_Click(object sender, EventArgs e)
        {
            habilitar_controles(true);
            cargar_fletes_pendientes_internet();
            cargar_fletes_pendientes_vigilancia();
            CBmasFletes.Enabled = true;
            CBProv.Enabled = true;

            try
            {
                thisConnection.Open();
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select max(id_ticket) from tb_mstr_recepcion_bascula";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    lastId = Convert.ToInt32(reader1.GetValue(0).ToString());
                }
                txtticket.Text = (lastId + 1).ToString();
                thisConnection.Close();

                btnAlta.Enabled = false;
                btnConsulta.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancel.Enabled = true;
                txtclaveprov.Enabled = true;
                opcion = 1;
                GBTipo.Enabled = true;
                GBVeh.Enabled = true;
                RBCar.Enabled = true;
                RBCaVe.Enabled = true;
                RBFlete.Enabled = true;
                RBMovInt.Enabled = true;
                RBProvExt.Enabled = true;
                RBvac.Enabled = true;
                txtticket.Enabled = true;
                txtticket.ReadOnly = false;
                txtpesohora.Enabled = true;
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show("Error al cargar el numero de ticket: " + ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //cuando se selecciona la linea por nombre
        private void CBlinea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (prov_clave.Trim() != "")
            {
                try
                {
                    thisConnection.Open();
                    lin = CBlinea.SelectedItem.ToString().Trim();
                    lin = lin.Replace("'", "''");
                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select lin_clave from tb_cat_linea where lin_nombre = '" + lin + "'";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        txtlin_clave.Text = reader1.GetValue(0).ToString().Trim();
                    }
                    reader1.Close();

                    CBProducto.Items.Clear();
                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select prod_nombre from tb_cat_producto where lin_clave = '" + txtlin_clave.Text + "'  AND prod_tipo <> 'ING' and estatus = 'A' order by prod_nombre";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        CBProducto.Items.Add(reader1.GetValue(0).ToString().Trim());
                    }
                    reader1.Dispose();

                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select vari_nombre from tb_cat_variedad where lin_clave = '" + txtlin_clave.Text + "' order by vari_nombre";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        variedad.Add(reader1.GetValue(0).ToString());
                    }
                    reader1.Close();
                    thisConnection.Close();
                    cargar_unidades(2);
                }
                catch (SqlException ex)
                {
                    thisConnection.Close();
                    MessageBox.Show(ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un proveedor, de lo contrario no se mostraran nada en rancho y por consiguiente en tabla tampoco", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CBProv.Focus();
                return;
            }
        }

        //cuando se teclea la clave de la linea
        private void txtlin_clave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (prov_clave.Trim() != "")
                {
                    try
                    {
                        thisConnection.Open();
                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "select lin_nombre from tb_cat_linea where lin_clave = '" + txtlin_clave.Text + "' order by lin_nombre";
                        reader1 = cmnd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            CBlinea.SelectedItem = reader1.GetValue(0).ToString().Trim();
                            //CBLinIni.SelectedItem = CBLinIni.SelectedIndex;
                            lin = reader1.GetValue(0).ToString().Trim();
                        }

                        CBProducto.Items.Clear();
                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "select prod_nombre from tb_cat_producto where lin_clave = '" + txtlin_clave.Text + "' AND prod_tipo <> 'ING' and estatus = 'A' order by prod_nombre";
                        reader1 = cmnd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            CBProducto.Items.Add(reader1.GetValue(0).ToString().Trim());
                        }

                        reader1.Dispose();

                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "select vari_nombre from tb_cat_variedad where lin_clave = '" + txtlin_clave.Text + "' order by vari_nombre";
                        reader1 = cmnd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            variedad.Add(reader1.GetValue(0).ToString());
                        }

                        reader1.Close();
                        thisConnection.Close();
                        cargar_unidades(2);
                    }
                    catch (SqlException ex)
                    {
                        thisConnection.Close();
                        MessageBox.Show(ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un proveedor, de lo contrario no se mostraran nada en rancho y por consiguiente en tabla tampoco", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CBProv.Focus();
                    return;
                }
            }
        }

        //cuando se selecciona el nombre del producto
        private void CBProducto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                aux_opcion = "V";
                thisConnection.Open();
                prod = CBProducto.SelectedItem.ToString().Trim();
                prod = prod.Replace("'", "''");
                cmnd1 = thisConnection.CreateCommand();
                //cmnd1.CommandText = "select prod_clave, prod_tipo, prod_presentacion from tb_cat_producto where prod_nombre like '%" + prod + "%' and lin_clave = '" + txtlin_clave.Text + "'";
                cmnd1.CommandText = "select prod_clave, prod_tipo, prod_presentacion from tb_cat_producto where prod_nombre like '" + prod + "' and lin_clave = '" + txtlin_clave.Text + "'";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    txtprod_clave.Text = reader1.GetValue(0).ToString().Trim();
                    tipo = reader1.GetValue(1).ToString().Trim();
                    envase = reader1.GetValue(2).ToString().Trim();
                }
                reader1.Close();
                thisConnection.Close();
                cargar_unidades(2);
                datos_producto();
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //cuando se tecela la clave del producto
        private void txtprod_clave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (CBProducto.Items.Count > 0)
                {
                    try
                    {
                        thisConnection.Open();
                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "select prod_nombre, prod_tipo, prod_presentacion from tb_cat_producto where prod_clave = '" + txtprod_clave.Text + "'and lin_clave = '" + txtlin_clave.Text + "'";
                        reader1 = cmnd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            CBProducto.SelectedItem = reader1.GetValue(0).ToString().Trim();
                            prod = reader1.GetValue(0).ToString().Trim();
                            tipo = reader1.GetValue(1).ToString().Trim();
                            envase = reader1.GetValue(2).ToString().Trim();
                        }
                        if (reader1.HasRows == false)
                        {
                            thisConnection.Close();
                            MessageBox.Show("No se encontro ningún producto con esta clave, favor de verificarla", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        reader1.Close();
                        thisConnection.Close();
                        cargar_unidades(2);
                        datos_producto();
                        aux_opcion = "V";
                    }
                    catch (SqlException ex)
                    {
                        thisConnection.Close();
                        MessageBox.Show(ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar la linea del producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        //al dar doble click trae los datos del flete, los datos se encuentra en Internet
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            cargar_unidades(2);
            consulta = "N";
            aux_opcion = "I";
            DGV1.Columns["Column17"].Visible = false;
            DGV1.Columns["vari"].Visible = true;

            for (int i = 0; i < variedad.Count; i++)
            {
                Column17.Items.Add(variedad[i]);
            }
            for (int i = 0; i < unidades.Count; i++)
            {
                Columna06.Items.Add(unidades[i]);
            }

            if (CBmasFletes.Checked != true)
            {
                this.DGV1.Rows.Clear();
            }
            String cad;
            cad = listBox1.SelectedItem.ToString().Trim();
            //CBlinea.Enabled = false;
            //txtlin_clave.Enabled = false;
            //CBProducto.Enabled = false;
            //txtprod_clave.Enabled = false;
            CBlinea.SelectedIndex = -1;
            //txtlin_clave.Clear();
            CBProducto.SelectedIndex = -1;
            txtprod_clave.Clear();

            try
            {
                numero = Convert.ToInt32(cad);
                numflete = cad.ToString().Trim();

                if (CBmasFletes.Checked != true)
                    txtregent.Text = numflete;
                else
                    txtregent.Text = txtregent.Text + ", " + numflete;

                if (numflete.Trim() != "")
                    btnimpticket.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Seleccion incorrecta");
                return;
            }
            cont++;
            try
            {
                mySqlConn.Open();
                thisConnection.Open();
                cmnd = mySqlConn.CreateCommand();
                cmnd.CommandText = "select A.observacion, B.nom_transportista, C.nom_rancho, A.valor_operador, A.valor_transportista, A.id_rancho, A.id_proveedor from tb_mstr_flete A, tb_cat_transportistas B, tb_cat_rancho C where A.id_flete = " + numero + " and B.id_transportista = A.valor_transportista and C.id_rancho = A.id_rancho and C.id_proveedor = A.id_proveedor";
                reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    if (CBmasFletes.Checked != true)
                    {
                        txtobs.Text = reader.GetValue(0).ToString().Trim();
                        txttrans.Text = reader.GetValue(1).ToString().Trim();
                        txtrch.Text = reader.GetValue(5).ToString().Trim() + " - " + reader.GetValue(2).ToString().Trim();
                        rch_clave = reader.GetValue(5).ToString().Trim();
                        aux_chofer = reader.GetValue(3).ToString().Trim();
                        aux_transporte = reader.GetValue(4).ToString().Trim();
                    }
                    else
                    {
                        txtobs.Text = txtobs.Text + ", " + reader.GetValue(0).ToString().Trim();
                        txttrans.Text = txttrans.Text + ", " + reader.GetValue(1).ToString().Trim();
                        txtrch.Text = txtrch.Text + ", " + reader.GetValue(2).ToString().Trim();
                    }
                    prov_clave = reader.GetValue(6).ToString().Trim();
                    txtclaveprov.Text = prov_clave;
                }
                reader.Dispose();

                cmnd = mySqlConn.CreateCommand();
                cmnd.CommandText = "select nom_operador from tb_cat_operadores where id_transportista = '" + aux_transporte + "' and id_operador = '" + aux_chofer + "'";
                reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    if (CBmasFletes.Checked != true)
                        txttrans.Text = txttrans.Text + ", " + reader.GetValue(0).ToString().Trim();
                    else
                        txttrans.Text = txttrans.Text + ", " + reader.GetValue(0).ToString().Trim();
                }
                reader.Dispose();

                cmnd = mySqlConn.CreateCommand();
                cmnd.CommandText = "select tipo_prod, cantidad, id_producto, id_linea, id_tabla, nom_tabla, num_tarimas, caj_tarimas, id_unidad, id_variedad, det_observa from tb_det_flete where id_flete = " + numero;
                reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    id_unidad = reader.GetValue(8).ToString().Trim();
                    for (int i = 0; i < clave_unidades.Count; i++)
                    {
                        if (id_unidad == clave_unidades[i].ToString().Trim())
                        {
                            nombre_unidad = unidades[i].ToString().Trim();
                            break;
                        }
                    }

                    id_variedad = reader.GetValue(9).ToString().Trim();
                    tabl = reader.GetValue(4).ToString().Trim();
                    tbl_nombre = reader.GetValue(5).ToString().Trim();
                    DGV1.Rows.Add(reader.GetValue(2).ToString().Trim(), reader.GetValue(3).ToString().Trim(), "", reader.GetValue(1).ToString(), nombre_unidad, "0", "0", "0", reader.GetValue(4).ToString().Trim(), reader.GetValue(5).ToString().Trim(), reader.GetValue(6).ToString().Trim(), reader.GetValue(7).ToString().Trim(), "", "", id_variedad, reader.GetValue(10).ToString().Trim(), reader.GetValue(2).ToString().Trim(), numero, prov_clave, rch_clave);
                }
                reader.Dispose();

                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select prov_nombre from tb_cat_proveedor where prov_clave = '" + prov_clave + "'";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    CBProv.SelectedItem = reader1.GetValue(0).ToString();
                }
                reader1.Dispose();

                for (int i = 0; i < DGV1.Rows.Count; i++)
                {
                    if (Convert.ToString(DGV1.Rows[i].Cells[0].Value).Length > 5)
                        DGV1.Rows[i].Cells[12].Value = "PT";

                    if (Convert.ToString(DGV1.Rows[i].Cells[0].Value).Length <= 5)
                        DGV1.Rows[i].Cells[12].Value = "MP";

                    //DGV1.Rows[i].Cells[0].Value = "09015";
                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select lin_clave, prod_nombre from tb_cat_producto where prod_clave = '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "'";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        DGV1.Rows[i].Cells[1].Value = reader1.GetValue(0).ToString().Trim();
                        DGV1.Rows[i].Cells[2].Value = reader1.GetValue(1).ToString().Trim();
                    }

                    if (reader1.HasRows == false)
                    {
                        cmnd = mySqlConn.CreateCommand();
                        if (Convert.ToString(DGV1.Rows[i].Cells[1].Value).Length <= 5)
                            cmnd.CommandText = "select A.id_producto, A.descripcion, A.id_linea from tb_cat_producto A where A.id_linea_ofc = '" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "' and tipo ='MP' limit 1";

                        else
                            cmnd.CommandText = "select A.id_producto, A.descripcion, A.id_linea from tb_cat_producto A where A.id_linea = '" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "' and A.id_producto = '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "' AND tipo ='PT'";

                        reader = cmnd.ExecuteReader();
                        while (reader.Read())
                        {
                            DGV1.Rows[i].Cells[0].Value = reader.GetValue(0).ToString().Trim();
                            DGV1.Rows[i].Cells[2].Value = reader.GetValue(1).ToString().Trim();
                            DGV1.Rows[i].Cells[1].Value = reader.GetValue(2).ToString().Trim();
                        }
                        reader.Dispose();
                    }
                    reader1.Dispose();

                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select vari_nombre from tb_cat_variedad where vari_clave = '" + Convert.ToString(DGV1.Rows[i].Cells[14].Value) + "' and lin_clave = '" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "'";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        DGV1.Rows[i].Cells[14].Value = reader1.GetValue(0).ToString().Trim();
                    }
                    reader1.Dispose();

                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select prod_presentacion, lin_clave, prod_nombre from tb_cat_producto where prod_clave = '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "'";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        cmnd11 = thisConnection.CreateCommand();
                        cmnd11.CommandText = "select env_nombre from tb_cat_envases where env_clave = '" + reader1.GetValue(0).ToString().Trim() + "'";
                        reader11 = cmnd11.ExecuteReader();
                        while (reader11.Read())
                        {
                            DGV1.Rows[i].Cells[4].Value = reader11.GetValue(0).ToString().Trim();
                        }
                        reader11.Dispose();
                        DGV1.Rows[i].Cells[1].Value = reader1.GetValue(1).ToString().Trim();
                        DGV1.Rows[i].Cells[2].Value = reader1.GetValue(2).ToString().Trim();
                    }
                    reader1.Dispose();
                }//for

                //DGV1.Columns["folio"].ReadOnly = true;
                thisConnection.Close();
                mySqlConn.Close();
                btnGuardar.Enabled = true;
                btnAlta.Enabled = false;
                btnConsulta.Enabled = false;
                lbindrev.Visible = true;
                lbIndError.Visible = true;
                CBIndRev.Visible = true;
                CBIndRev.SelectedIndex = 0;
                txtIndError.Visible = true;
                txtIndError.Enabled = true;
                CBIndRev.Enabled = true;
            }
            catch (MySqlException ex)
            {
                thisConnection.Close();
                mySqlConn.Close();
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        void CargarFlete()                      ///////////// Carga el Flete WEB ///////////////////////////
        {
            numero_renglones = 0;
            texto = "";
            texto = texto + "                  GRUPO U   \r\n\r\n"; numero_renglones++; numero_renglones++; numero_renglones++;
            texto = texto + "  Orden de Flete: " + numflete.ToString() + "\r\n"; numero_renglones++;
            String cve_responsable = "";
            String txt_chofer = "";
            String txt_placas = "";
            this.DGV1.Rows.Clear();
            MySqlCommand cmnd1 = mySqlConn.CreateCommand();
            MySqlDataReader reader;
            cmnd1.CommandText = "SELECT * FROM tb_mstr_flete WHERE id_flete = " + Convert.ToInt32(numflete);
            try
            {
                mySqlConn.Open();
                reader = cmnd1.ExecuteReader();
                while (reader.Read())
                {
                    id_proveedor = reader.GetString(3); aux_id_proveedor = reader.GetString(3); //textBoxIdProv.Text = aux_id_proveedor;
                    id_rancho = reader.GetString(4); aux_id_rancho = reader.GetString(4); //textBoxIdRan.Text = aux_id_rancho;
                    origen = reader.GetString(27); aux_od = reader.GetString(27);
                    txtobs.Text = reader.GetString(11); aux_observacion = reader.GetString(11);
                    id_responsable = reader.GetString(22); aux_id_responsable = reader.GetString(22); //textBox10.Text = aux_id_responsable;
                    cve_responsable = reader.GetString(22);
                    txt_chofer = reader.GetString(18);
                    txt_placas = reader.GetString(17);
                    aux_id_tipo_flete = reader.GetString(21);
                    aux_valor_transportista = reader.GetString(31);
                    fecha_carga = reader.GetString(9);

                    if (reader.GetString(28).ToString().Equals("P"))
                    {
                        plataforma = "Plataforma";
                        tipo_transporte = "Plataforma";
                        aux_num_economico = reader.GetString(29).ToString();
                        aux_valor_operador = reader.GetString(30);
                        aux_chofer = "Plataforma";
                    }
                    else
                    {
                        numero_economico = txt_placas.ToString(); aux_num_economico = txt_placas.ToString();
                        aux_valor_operador = reader.GetString(31);
                        chofer = txt_chofer.ToString(); aux_chofer = chofer;
                    }
                    if (reader.GetString(28).ToString().Equals("R")) { tipo_transporte = "Rabon"; }
                    if (reader.GetString(28).ToString().Equals("T")) { tipo_transporte = "Torton"; }
                    if (reader.GetString(28).ToString().Equals("A")) { tipo_transporte = "Tractor"; }
                    if (reader.GetString(28).ToString().Equals("G")) { tipo_transporte = "Gondula"; }
                    if (reader.GetString(28).ToString().Equals("O")) { tipo_transporte = "Otro"; }

                    aux_transporte = tipo_transporte;
                    //aux_tipo_transp = textBox6.Text.ToString();



                }
                cmnd1.Dispose();
                reader.Dispose();

                this.DGVtemporal.Rows.Clear();
                MySqlCommand cmnd2 = mySqlConn.CreateCommand();
                MySqlDataReader reader2;
                cmnd2.CommandText = "SELECT * FROM tb_det_flete WHERE id_flete = " + Convert.ToInt32(numflete);
                reader2 = cmnd2.ExecuteReader();
                while (reader2.Read())
                {
                    if (reader2.GetString(2).Length > 2)
                    {
                        this.DGVtemporal.Rows.Add(false, reader2.GetString(5).ToString(), reader2.GetString(3).ToString(), reader2.GetString(2).ToString(), reader2.GetString(5).ToString(), "", "", "", "", "", reader2.GetString(10).ToString(), reader2.GetString(24).ToString(), reader2.GetString(9).ToString(), reader2.GetString(30).ToString(), reader2.GetString(31).ToString(), "MP", reader2.GetString(6).ToString(), reader2.GetString(28).ToString(), reader2.GetString(29).ToString(), reader2.GetString(7).ToString());
                    }
                    else
                    {
                        this.DGVtemporal.Rows.Add(false, reader2.GetString(5).ToString(), reader2.GetString(3).ToString(), reader2.GetString(2).ToString(), reader2.GetString(5).ToString(), "", "", "", "", "", reader2.GetString(10).ToString(), reader2.GetString(24).ToString(), reader2.GetString(9).ToString(), reader2.GetString(30).ToString(), reader2.GetString(31).ToString(), "PT", reader2.GetString(6).ToString(), reader2.GetString(28).ToString(), reader2.GetString(29).ToString(), reader2.GetString(7).ToString());
                    }
                    textBox10.Text = reader2.GetString(24);

                    id_rancho = reader2.GetString(23);
                    id_responsable = reader2.GetString(22); aux_nombre_proveedor = reader2.GetString(22);
                    id_linea = reader2.GetString(25); aux_origen = reader2.GetString(25);

                }
                cmnd2.Dispose();
                reader2.Dispose();


                MySqlCommand cmnd3 = mySqlConn.CreateCommand();
                MySqlDataReader reader3;
                cmnd3.CommandText = "SELECT nom_responsable FROM tb_cat_responsables WHERE id_responsable = '" + cve_responsable + "'";

                reader3 = cmnd3.ExecuteReader();
                while (reader3.Read())
                {
                    //textBox9.Text = reader3.GetString(0);
                    aux_responsable = reader3.GetString(0);
                }
                cmnd3.Dispose();
                reader3.Dispose();

                MySqlCommand cmndOD = mySqlConn.CreateCommand();
                MySqlDataReader readerOD;
                cmndOD.CommandText = "SELECT orde FROM tb_cat_origen_destino WHERE id_od = '" + aux_od.ToString() + "'";
                readerOD = cmndOD.ExecuteReader();
                while (readerOD.Read())
                {
                    texto = texto + "  Destino: " + readerOD.GetString(0) + "\r\n";
                }
                cmndOD.Dispose();
                readerOD.Dispose();


                MySqlCommand cmnd4 = mySqlConn.CreateCommand();
                MySqlDataReader reader4;
                cmnd4.CommandText = "SELECT nom_proveedor FROM tb_cat_proveedor WHERE id_proveedor = '" + id_proveedor + "'";
                reader4 = cmnd4.ExecuteReader();
                while (reader4.Read())
                {
                    texto = texto + "  Proveedor: " + reader4.GetString(0) + "\r\n";
                }
                cmnd4.Dispose();
                reader4.Dispose();

                MySqlCommand cmnd5 = mySqlConn.CreateCommand();
                MySqlDataReader reader5;
                cmnd5.CommandText = "SELECT nom_rancho FROM tb_cat_rancho WHERE id_proveedor = '" + id_proveedor + "' AND id_rancho ='" + aux_id_rancho + "'";
                reader5 = cmnd5.ExecuteReader();
                while (reader5.Read())
                {
                    texto = texto + "  Rancho: " + reader5.GetString(0) + "\r\n";
                    txtrch.Text = reader5.GetString(0).ToString();
                }
                cmnd5.Dispose();
                reader5.Dispose();

                texto = texto + "  Fecha Carga : " + fecha_carga + "\r\n";
                texto = texto + "  Movimiento FLETES COSECHA \r\n"; numero_renglones++;
                texto = texto + "  Camion Tipo: " + aux_transporte + "\r\n"; numero_renglones++;

                texto = texto + "\r\n Responsable: " + aux_responsable.ToString() + "\r\n\r\n"; numero_renglones++; numero_renglones++; numero_renglones++;
                texto = texto + " Transporta: " + aux_chofer.ToString() + "\r\n"; numero_renglones++;
                texto = texto + " Num Economico: " + aux_num_economico.ToString() + "\r\n"; numero_renglones++;

                if (aux_transporte.Equals("Plataforma"))
                {
                    MySqlCommand cmnd6 = mySqlConn.CreateCommand();
                    MySqlDataReader reader6;
                    cmnd6.CommandText = "SELECT nom_operador FROM tb_cat_operadores WHERE id_operador = '" + aux_valor_operador.ToString() + "'";
                    reader6 = cmnd6.ExecuteReader();
                    while (reader6.Read())
                    {
                        texto = texto + " Operador: " + reader6.GetString(0) + "\r\n\r\n"; numero_renglones++; numero_renglones++;
                        txttrans.Text = reader6.GetString(0).ToString();
                    }
                    cmnd6.Dispose();
                    reader6.Dispose();
                }
                else
                {
                    MySqlCommand cmnd7 = mySqlConn.CreateCommand();
                    MySqlDataReader reader7;
                    cmnd7.CommandText = "SELECT nom_transportista FROM tb_cat_transportistas WHERE id_transportista = '" + aux_valor_operador.ToString() + "'";
                    reader7 = cmnd7.ExecuteReader();
                    while (reader7.Read())
                    {
                        texto = texto + " Operador: " + reader7.GetString(0) + "\r\n\r\n"; numero_renglones++; numero_renglones++;
                    }
                    cmnd7.Dispose();
                    reader7.Dispose();
                }
                texto = texto + " Productos en Flete \r\n --------  --------  --------  --------\r\n"; numero_renglones++; numero_renglones++;
                mySqlConn.Close();
                actualizarGridFletes();
            }
            catch (MySqlException ex)
            {
                mySqlConn.Close();
                //MessageBox.Show("Error Conectando al sitio Fletes Mrlucky\n", "ERROR");
                MessageBox.Show(ex.ToString());
            }
            //actualizarGridFletes();
            //comboBox2.Visible = false;
            //comboBox3.Visible = false;
            //comboBox4.Visible = false;
            //comboBox5.Visible = false;
        }

        void actualizarGridFletes()
        {
            string aux_nombre, aux_linea, indica, texto2;
            rencontrol = 5; aux_nom_variedad = "";
            texto2 = ""; tant = "";
            for (int rows = 0; rows < DGVtemporal.Rows.Count; rows++)
            {
                indica = "N"; rencontrol++;
                // if (rencontrol == 5) { texto2 = ""; }
                string aux_codigo = DGVtemporal.Rows[rows].Cells[2].Value.ToString();

                aux_nombre = ""; aux_linea = "";

                try { thisConnection.Open(); }
                catch { thisConnection.Close(); thisConnection.Open(); }

                try
                {
                    SqlCommand cmnd1 = thisConnection.CreateCommand();
                    SqlDataReader reader;

                    cmnd1.CommandText = "SELECT * FROM tb_cat_weblinea WHERE id_linea_ofc = " + Convert.ToInt16(aux_codigo);
                    reader = cmnd1.ExecuteReader();
                    while (reader.Read())
                    {
                        aux_nombre = reader.GetValue(2).ToString();
                        aux_linea = reader.GetValue(0).ToString();
                        indica = "S";
                    }
                    cmnd1.Dispose();
                    reader.Dispose();

                    SqlCommand cmnd2 = thisConnection.CreateCommand();
                    SqlDataReader reader2;
                    cmnd2.CommandText = "SELECT vari_clave, lin_clave, vari_nombre FROM  tb_cat_variedad WHERE vari_clave = '" + Convert.ToString(DGVtemporal.Rows[rows].Cells[16].Value) + "' AND lin_clave  = '" + aux_linea.ToString().Trim() + "'";
                    reader2 = cmnd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        aux_nom_variedad = reader2.GetString(2).ToString();
                    }
                    cmnd2.Dispose();
                    reader2.Dispose();
                    thisConnection.Close();
                }
                catch
                {
                    thisConnection.Close();
                }
                if (indica.Equals("S"))
                {
                    texto2 = texto2 + " Linea: Materia Prima \r\n"; //numero_renglones++;
                    texto2 = texto2 + " Tabla: " + DGVtemporal.Rows[rows].Cells[11].Value.ToString() + "\r\n";
                    //texto2 = texto2 + " Variedad: " + DGVtemporal.Rows[rows].Cells[16].Value.ToString() + " -> " + aux_nom_variedad.ToString() + "\r\n";
                    texto2 = texto2 + " Variedad: " + DGVtemporal.Rows[rows].Cells[16].Value.ToString() + "\r\n";
                    texto2 = texto2 + " Cantidad: " + DGVtemporal.Rows[rows].Cells[1].Value.ToString() + "\r\n";
                    texto2 = texto2 + aux_nombre.ToString() + "\r\n--------  -------  --------  -------\r\n";

                    DGVtemporal.Rows[rows].Cells[3].Value = aux_linea.ToString();
                    DGVtemporal.Rows[rows].Cells[4].Value = aux_nombre.ToString();
                }
                else
                {
                    texto2 = texto2 + " Linea: Producto Terminado \r\n";
                    texto2 = texto2 + " Tabla: " + DGVtemporal.Rows[rows].Cells[11].Value.ToString() + "\r\n";
                    //texto2 = texto2 + " Variedad: " + DGVtemporal.Rows[rows].Cells[16].Value.ToString() + " -> " + aux_nom_variedad.ToString() + "\r\n";
                    texto2 = texto2 + " Variedad: " + DGVtemporal.Rows[rows].Cells[16].Value.ToString() + "\r\n";
                    texto2 = texto2 + " Cantidad: " + DGVtemporal.Rows[rows].Cells[1].Value.ToString() + "\r\n";

                    try { thisConnection.Open(); }
                    catch { thisConnection.Close(); thisConnection.Open(); }

                    SqlCommand cmnd2 = thisConnection.CreateCommand();
                    SqlDataReader reader2;
                    cmnd2.CommandText = "SELECT * FROM tb_cat_producto WHERE prod_clave = '" + aux_codigo.ToString().Trim() + "'";
                    reader2 = cmnd2.ExecuteReader();
                    while (reader2.Read())
                    {

                        //aux_presentacion = reader2.GetValue(10).ToString();
                        aux_presentacion = reader2.GetString(10).ToString();
                        if (reader2.GetString(10).Trim().ToString().Length > 0) { aux_presentacion = reader2.GetString(10).ToString(); }
                        else { aux_presentacion = "05"; }

                        aux_nombre = reader2.GetValue(2).ToString();
                        aux_linea = reader2.GetValue(3).ToString();
                    }
                    cmnd2.Dispose();
                    reader2.Dispose();
                    thisConnection.Close();
                    //dataGridView1.Rows[rows].Cells[3].Value = aux_linea.ToString();
                    //dataGridView1.Rows[rows].Cells[4].Value = aux_nombre.ToString();
                    //dataGridView1.Rows[rows].Cells[5].Value = aux_presentacion.ToString();
                    texto2 = texto2 + aux_nombre.ToString() + "\r\n--------  -------  --------  -------\r\n";
                }
                tant = tant + texto2; texto2 = "";
                //if (rencontrol <= 4) { tant = tant + texto2; texto2 = "";  }
                //if ((rencontrol > 4) && (rencontrol == dataGridView1.Rows.Count)) { tpos = texto2; }
                numero_renglones++;
            }  //for

            texto = texto + tant;
            texto = texto + aux_observacion + "\r\n\r\n";
            texto = texto + "Copia realizada en Comercializadora GAB\r\n\r\n";

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(numflete);

            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new FileStream(@"C:\SisGabWeb\qrcode.png", FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            //if (rencontrol <= 4) { texto = texto + "\r\n\r\n" + "Copia realizada en Comercializadora GAB\r\n\r\n\r\n\r\n"; }
            //else
            //{
            //    textosig = tpos + "\r\n\r\n" + "Copia realizada en Comercializadora GAB\r\n\r\n\r\n\r\n"; numero_renglones = 1;
            // }
        }

        private void btnimpticket_Click(object sender, EventArgs e)
        {
            if (consulta == "N")
            {
                CargarFlete();
                int largo, ancho;
                ancho = 300;
                //largo = rencontrol * 105;
                largo = 1100;
                PrintDocument formulario = new PrintDocument();
                formulario.PrintPage += new PrintPageEventHandler(Datos_Cliente);
                formulario.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("BASCULA", ancho, largo); // defines el tamaño del papel
                //formulario.PrinterSettings.PrinterName = formulario.PrinterSettings.IsDefaultPrinter;
                //formulario.PrinterSettings.PrinterName = "ZDesigner TLP 2844";
                //formulario.PrinterSettings.PrinterName = "ZDesigner TLP 2844 (Copiar 1)";
                //formulario.PrinterSettings.PrinterName = "PDFCreator";
                for (int i = 0; i < 4; i++)
                {
                    formulario.Print();
                }
            }
            else
            {
                a = 0;
                for (a = 0; a < DGV1.Rows.Count; a++)
                {
                    if (Convert.ToString(DGV1.Rows[a].Cells[1].Value).Trim() == "08")
                    {
                        thisConnection.Open();
                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "select rmp_recibo from tb_mstr_recepcion_esparrago where rmp_ticket = '" + Convert.ToString(txtticket.Text) + "'";
                        rec_esp = Convert.ToString(cmnd1.ExecuteScalar());
                        thisConnection.Close();
                        PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                        pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 307, 393);
                        pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                        //PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
                        //VistaPrevia.Document = pd;
                        //VistaPrevia.ShowDialog();
                        pd.Print();
                        i = 1;
                    }
                }
            }
            btnimpticket.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancel.Enabled = false;
            listBox1.Items.Clear();
            limpiarTextBoxes(this);
            btnmodi.Enabled = false;
            btnimpticket.Text = "IMPRIMIR TICKET FLETE";
        }

        //combobox donde se muestran los proveedores que captura vigilancia
        private void CBRegVig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Int32 id_entrada = Convert.ToInt32(CBRegVig.SelectedValue.ToString());
                thisConnection.Open();
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select nombre, id_entrada from tb_mstr_registros_vigilancia where id_entrada = " + id_entrada;
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    txttrans.Text = reader1.GetValue(0).ToString();
                    txtregent.Text = reader1.GetValue(1).ToString().Trim();
                }
                reader1.Close();
                aux_opcion = "V";
                thisConnection.Close();
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void datos_producto()
        {
            try
            {
                for (int i = 0; i < variedad.Count; i++)
                {
                    Column17.Items.Add(variedad[i]);
                }
                for (int i = 0; i < unidades.Count; i++)
                {
                    Columna06.Items.Add(unidades[i]);
                }
                //ran.SelectedIndex = -1;

                //ranch.Items.Clear();
                //for (int i = 0; i < ranchos.Items.Count; i++)
                //{
                //    ranch.Items.Add(ranchos.Items[i]);
                //}

                //bool esta = false;

                for (int i = 0; i < clave_unidades.Count; i++)
                {
                    if (envase == clave_unidades[i].ToString().Trim())
                    {
                        nombre_unidad = unidades[i].ToString().Trim();
                        break;
                    }
                }

                //if (DGV1.Rows.Count > 0)
                //{
                //    for (int i = 0; i < DGV1.Rows.Count; i++)
                //    {
                //        if (txtprod_clave.Text.Trim() == Convert.ToString(DGV1.Rows[i].Cells[0].Value))
                //        {
                //            esta = true;
                //            break;
                //        }
                //    }

                //    if (esta == true)
                //    {
                //        esta = false;
                //        MessageBox.Show("Este producto ya ha sido agregado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }
                //    else
                //    {
                //        DGV1.Rows.Add(txtprod_clave.Text, txtlin_clave.Text, CBProducto.SelectedItem.ToString().Trim(), 0, nombre_unidad, 0, 0, 0, "", "", 0, 0, tipo, "", "");
                //    }
                //}
                //else  
                //thisConnection.Open();
                //cmnd1 = thisConnection.CreateCommand();
                //cmnd1.CommandText = "select rch_clave from tb_cat_ranchos where prov_clave ='" + prov_clave + "'";
                //reader1 = cmnd1.ExecuteReader();

                //if (reader1.HasRows)
                //{
                //    while (reader1.Read())
                //        rch_clave = reader1.GetValue(0).ToString().Trim();
                //}
                //else
                //    rch_clave = "01";

                //reader1.Dispose();

                //cmnd1 = thisConnection.CreateCommand();
                //cmnd1.CommandText = "select tbl_clave, tbl_nombre from tb_cat_tablas where prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'";
                //reader1 = cmnd1.ExecuteReader();
                //if (reader1.HasRows)
                //{
                //    while (reader1.Read())
                //    {
                //        tbl_clave = reader1.GetValue(0).ToString().Trim();
                //        tbl_nombre = reader1.GetValue(1).ToString().Trim();
                //    }
                //}
                //else
                //{
                //    tbl_clave = "01";
                //    tbl_nombre = "";
                //}

                //reader1.Dispose();
                //thisConnection.Close();
                Column12.Items.Clear();
                tbl.Items.Clear();
                DGV1.Refresh();
                DGV1.Rows.Add(txtprod_clave.Text.Trim(), txtlin_clave.Text.Trim(), CBProducto.SelectedItem.ToString().Trim(), 0, nombre_unidad, 0, 0, 0, tabl, tbl_nombre, 0, 0, tipo, "", "", "", "", numflete, prov_clave, rch_clave);
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void DGV1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "El valor de DataGridViewComboBoxCell no es válido." || e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                object value = DGV1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)DGV1.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)DGV1.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
        }

        private void cargar_unidades(int lugar)
        {
            #region cuando el flete se carga de internet
            if (lugar == 1)
            {
                try
                {
                    mySqlConn.Open();
                    cmnd = mySqlConn.CreateCommand();
                    cmnd.CommandText = "select desc_unidad, id_unidad from tb_cat_unidades";
                    reader = cmnd.ExecuteReader();
                    while (reader.Read())
                    {
                        unidades.Add(reader.GetValue(0).ToString());
                        clave_unidades.Add(reader.GetValue(1).ToString());
                    }
                    reader.Dispose();
                    mySqlConn.Close();
                }
                catch (MySqlException ex)
                {
                    mySqlConn.Close();
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            #endregion           

            #region cuando el flete se carga manual
            if (lugar == 2)
            {
                try
                {
                    thisConnection.Open();
                    cmnd1 = thisConnection.CreateCommand();
                    //cmnd1.CommandText = "select um_nombre from tb_cat_unidad order by um_nombre";
                    //cmnd1.CommandText = "SELECT env_nombre, env_clave FROM tb_cat_envases WHERE env_peso <> 0 ORDER BY env_nombre";
                    cmnd1.CommandText = "SELECT env_nombre, env_clave FROM tb_cat_envases ORDER BY env_nombre";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        unidades.Add(reader1.GetValue(0).ToString());
                        clave_unidades.Add(reader1.GetValue(1).ToString().Trim());
                        //envases.Items.Add(reader1.GetValue(1).ToString());
                    }
                    reader1.Dispose();
                    thisConnection.Close();
                }
                catch (SqlException ex)
                {
                    thisConnection.Close();
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            #endregion
        }

        public int i = 1, a = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (i < 4)
            {
                SolidBrush color = new SolidBrush(Color.Black);
                Font fuente = new Font("Courier", 10);
                Font cod_bar = new Font("PF Barcode 39", 30);//código de barras                
                Point rec = new System.Drawing.Point(10, 10);
                e.Graphics.DrawString("Recibo: " + rec_esp, fuente, color, rec);

                Point tic = new Point(150, 10);
                e.Graphics.DrawString("Ticket: " + txtticket.Text, fuente, color, tic);

                Point fec = new System.Drawing.Point(10, 30);
                e.Graphics.DrawString("Fecha: " + DTPFecha.Value.ToShortDateString(), fuente, color, fec);

                Point prod = new System.Drawing.Point(10, 50);
                //e.Graphics.DrawString("Producto: " + CBProducto.SelectedItem.ToString().Trim(), fuente, color, prod);
                e.Graphics.DrawString("Producto: ESPARRAGO", fuente, color, prod);

                if (CBProv.SelectedItem.ToString().Length > 21)
                {
                    string uno = CBProv.SelectedItem.ToString().Substring(0, 19);
                    string dos = CBProv.SelectedItem.ToString().Substring(20, 19);
                    Point prov = new System.Drawing.Point(10, 70);
                    e.Graphics.DrawString("Proveedor: " + uno, fuente, color, prov);
                    Point prov2 = new System.Drawing.Point(10, 83);
                    e.Graphics.DrawString(dos, fuente, color, prov2);
                }
                else
                {
                    Point prov = new System.Drawing.Point(10, 70);
                    e.Graphics.DrawString("Proveedor: " + CBProv.SelectedItem.ToString().Trim(), fuente, color, prov);
                }

                Point rch = new System.Drawing.Point(10, 110);
                e.Graphics.DrawString("Rancho: " + Convert.ToString(DGV1.Rows[a].Cells["ranch"].Value), fuente, color, rch);

                Point can = new System.Drawing.Point(10, 130);
                e.Graphics.DrawString("Cantidad: " + Convert.ToString(DGV1.Rows[a].Cells[3].Value) + " " + Convert.ToString(DGV1.Rows[a].Cells[4].Value).Substring(0, 11), fuente, color, can);

                if (txtobs.TextLength > 24)
                {
                    string uno = txtobs.Text.Substring(0, 24);
                    string dos = txtobs.Text.Substring(24, 24);
                    Point obs = new System.Drawing.Point(10, 150);
                    e.Graphics.DrawString("Observaciones: \r\n" + uno, fuente, color, obs);
                    Point obs1 = new System.Drawing.Point(10, 178);
                    e.Graphics.DrawString(dos, fuente, color, obs1);
                }
                else
                {
                    Point obs = new System.Drawing.Point(10, 150);
                    e.Graphics.DrawString("Observaciones: \r\n" + txtobs.Text, fuente, color, obs);
                }

                //código de barras 
                Point bar_code = new Point(50, 230);
                e.Graphics.DrawString("*" + rec_esp + "*", cod_bar, color, bar_code);

                Point rec1 = new System.Drawing.Point(120, 270);
                e.Graphics.DrawString(rec_esp, fuente, color, rec1);

                Point hour = new System.Drawing.Point(230, 360);
                e.Graphics.DrawString("Hora: " + txtpesohora.Text, fuente, color, hour);
                i++;
            }
            if (i == 4)
            {
                e.HasMorePages = false;
                i = 1;
            }
            else
                e.HasMorePages = true;
        }

        //En esta función se define los datos a imprimir
        //los datos y la posición de los mismos en el documento
        private void Datos_Cliente(object obj, PrintPageEventArgs ev)
        {
            float pos_x = 15;
            float pos_y = 15;
            //ev.Graphics.DrawString("Nombre: ", fuente, Brushes.Black, pos_x, pos_y, new StringFormat());

            Image newImage = Image.FromFile(@"C:\SisGabWeb\qrcode.png");

            //cuando son 1 productos en el ticket
            if (numero_renglones == 16)
                coor_y_ima = 360;

            //cuando son 2 productos en el ticket
            if (numero_renglones == 17)
                coor_y_ima = 427;

            //cuando son 3 productos en el ticket
            if (numero_renglones == 18)
                coor_y_ima = 500;

            //cuando son 4 productos en el ticket
            if (numero_renglones == 19)
                coor_y_ima = 573;

            //cuando son 5 productos en el ticket
            if (numero_renglones == 20)
                coor_y_ima = 646;

            //cuando son 6 productos en el ticket
            if (numero_renglones == 21)
                coor_y_ima = 719;

            //cuando son 7 productos en el ticket
            if (numero_renglones == 22)
                coor_y_ima = 792;

            //si tiene observaciones
            if (aux_observacion.Trim() != "")
                coor_y_ima = 800;


            ev.Graphics.DrawImage(newImage, 15, coor_y_ima);
            ev.Graphics.DrawString(texto, fuente, Brushes.Black, pos_x + 6, pos_y, new StringFormat());
            ev.Graphics.DrawString("F-100-PAA-64\n\r Rev:02", fuente, Brushes.Black, 15, coor_y_ima + 150);
            numero_renglones = 0;
        }

        //botón cancelar
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CBlinea.Enabled = true;
            txtlin_clave.Enabled = true;
            CBProducto.Enabled = true;
            txtprod_clave.Enabled = true;
            DGV1.Rows.Clear();
            limpiarTextBoxes(this);
            btnmodi.Enabled = false;
            btnAlta.Focus();
            listBox1.Items.Clear();
            btnimpticket.Enabled = false;
            DTPFecha.Enabled = false;
            DTPFecha.Value = DateTime.Now;
            ran.Items.Clear();
            tbl.Items.Clear();
            Column12.Items.Clear();
            cont = 0;
            btnimpticket.Text = "IMPRIMIR TICKET FLETE";
            txtpesohora.Value = DateTime.Now;
            if (CmbTktPen.Enabled == false)
                CmbTktPen.Enabled = true;
            TicketsPendientes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (RBCar.Checked == true)
                vehiculo = "C";

            if (RBvac.Checked == true)
                vehiculo = "V";

            if (txtregent.Text.Trim() == "")
                txtregent.Text = "0";

            if (RBFlete.Checked == true)
                clasificacion = "F";

            if (RBProvExt.Checked == true)
                clasificacion = "E";

            if (RBCaVe.Checked == true)
                clasificacion = "C";

            if (RBMovInt.Checked == true)
                clasificacion = "I";

            if (txtpesohora.Text.Trim() == "" || txtpesohora.Text.Trim() == "00:00")
            {
                MessageBox.Show("Favor de capturar la hora de peso", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpesohora.Focus();
                return;
            }

            if (vehiculo.Trim() == "")
            {
                MessageBox.Show("Favor de seleccionar el vehiculo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RBCar.Focus();
                return;
            }

            if (clasificacion.Trim() == "")
            {
                MessageBox.Show("Favor de seleccionar la clasificacion", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RBFlete.Focus();
                return;
            }


            if (CBRecBas.SelectedIndex == -1)
            {
                MessageBox.Show("Favor de seleccionar el nombre de la persona de báscula", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CBRecBas.Focus();
                return;
            }

            if (CBIndRev.SelectedIndex == 0 || CBIndRev.SelectedIndex == -1)
                hay = "N";

            if (CBIndRev.SelectedIndex == 1)
                hay = "S";


            peso_entrada = Convert.ToDouble(txtpesoent.Text);
            peso_bruto = Convert.ToDouble(txtpesobruto.Text);
            tara = Convert.ToDouble(txttara.Text);
            peso_neto = Convert.ToDouble(txtpesoneto.Text);

            if (DGV1.Rows.Count > 0)
            {
                for (int i = 0; i < DGV1.Rows.Count; i++)
                {
                    if (aux_opcion == "I")
                    {
                        mySqlConn.Open();
                        cmnd = mySqlConn.CreateCommand();
                        cmnd.CommandText = "select count(*) from tb_mstr_flete where id_flete = " + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + " and " +
                                           "id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                        int existe_flete = Convert.ToInt32(cmnd.ExecuteScalar());
                        mySqlConn.Close();
                        if (existe_flete == 0)
                        {
                            MessageBox.Show("El proveedor y/o el flete no coinciden\r\nFavor de verificarlo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (Convert.ToString(DGV1.Rows[i].Cells["prove"].Value).Trim() != txtclaveprov.Text.Trim())
                    {
                        MessageBox.Show("El proveedor no coinciden\r\nFavor de verificarlo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    suma_prod = suma_prod + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value);
                    if (cont > 1)
                    {
                        productos = productos + Convert.ToString(DGV1.Rows[i].Cells[2].Value).Trim() + ", ";
                        unities = unities + Convert.ToString(DGV1.Rows[i].Cells[4].Value).Trim() + ", ";
                        //txttrans.Text = txttrans.Text.Trim().Substring(0, 90);
                    }
                    if (Convert.ToString(DGV1.Rows[i].Cells[5].Value).Trim() == "" || Convert.ToString(DGV1.Rows[i].Cells[6].Value) == "0" || Convert.ToString(DGV1.Rows[i].Cells[6].Value) == "0")
                    {
                        MessageBox.Show("Favor de escribir los pesos", "AVISOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DGV1.Rows[i].Selected = true;
                        return;
                    }

                    //if (Convert.ToDecimal(DGV1.Rows[i].Cells[5].Value) > 0)
                    //{
                    //    MessageBox.Show("El peso no puede ser negativo", "AVISOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    DGV1.Rows[i].Selected = true;
                    //    return;
                    //}

                    //if (Convert.ToDecimal(DGV1.Rows[i].Cells[6].Value) > 0)
                    //{
                    //    MessageBox.Show("El peso no puede ser negativo", "AVISOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    DGV1.Rows[i].Selected = true;
                    //    return;
                    //}

                    if (Convert.ToString(DGV1.Rows[i].Cells[4].Value) == "")
                    {
                        MessageBox.Show("Favor de seleccionar el envase", "AVISOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DGV1.Rows[i].Cells[4].Selected = true;
                        return;
                    }
                    if (Convert.ToString(DGV1.Rows[i].Cells[10].Value) == "0")// || Convert.ToString(DGV1.Rows[i].Cells[11].Value) == "0")
                    {
                        MessageBox.Show("Favor de escribir el número de tarimas", "AVISOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DGV1.Rows[i].Selected = true;
                        return;
                    }

                    if (DGV1.Rows[i].Cells[6].Value == null)
                    {
                        MessageBox.Show("Favor de seleccionar el envase", "AVISOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DGV1.Rows[i].Selected = true;
                        return;
                    }

                    if (Convert.ToString(DGV1.Rows[i].Cells[19].Value).Trim() == "" || Convert.ToString(DGV1.Rows[i].Cells[9].Value).Trim() == "")
                    {
                        if (MessageBox.Show("Falta seleccionar el rancho y/o la tabla\r\n¿Desea continuar guardando la información?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            DGV1.Rows[i].Selected = true;
                            return;
                        }
                        else
                        {

                        }
                    }
                }
                for (int i = 0; i < DGV1.Rows.Count; i++)
                {
                    if (Convert.ToString(DGV1.Rows[i].Cells[17].Value).Trim() == "")
                    {
                        if (MessageBox.Show("El producto de la fila: " + (i + 1).ToString() + " no tiene ticket\n\r¿Desea continuar guardando la información?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            DGV1.Focus();
                            DGV1.Rows[i].Selected = true;
                            return;
                        }
                        else
                        {
                        }
                    }
                }
            }

            if (MessageBox.Show("¿Esta seguro de guardar?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //MessageBox.Show(txtregent.Text.Replace(", ", "").Trim());
                #region guardar nuevo registro
                if (opcion == 1)
                {
                    TimeSpan dif_horas = DateTime.Now.Subtract(txtpesohora.Value);
                    if ((dif_horas.Minutes > 40) || (dif_horas.Hours >= 1))
                    {
                        MessageBox.Show("Favor de verificar la hora\r\nExcede de los 30 minutos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtpesohora.Focus();
                        return;
                    }


                    #region si el flete es de Internet
                    if (aux_opcion == "I")
                    {
                        #region un solo registro de báscula con más de un flete
                        if (CBmasFletes.Checked == true)
                        {
                            try
                            {
                                if (cont > 1)
                                {
                                    txtregent.Text = " ";
                                    if (txttrans.Text.Length > 99)
                                        txttrans.Text = txttrans.Text.Substring(0, 90);
                                }
                                fecha = DateTime.Now;
                                txtregent.Text = "";
                                thisConnection.Open();
                                string insertarSql = "DECLARE @Error INT BEGIN TRAN " +
                                                     "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, " +
                                                     "clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula, error, error_comenta, prov_clave, " +
                                                     "hora_peso) VALUES (@aux_fecha_entrada, @aux_cargado, @aux_folio, @aux_transportista, @aux_rancho, @aux_cantidad, @aux_unidad, " +
                                                     "@aux_producto, @aux_observacion, @aux_clasificacion, @aux_peso_entrada, @aux_peso_bruto, @aux_tara, @aux_peso_neto, @id_entrada, " +
                                                     "@aux_estatus, @id_bascula, @hay_error, @error_comenta, @prove_clave, @hor_pes) SELECT SCOPE_IDENTITY() " +
                                                     "SET @Error = @@ERROR IF( @Error <> 0) GOTO TratarError COMMIT TRAN TratarError: " +
                                                     "IF @@Error<>0 BEGIN PRINT 'ha ocurrido un error' ROLLBACK TRAN END";
                                //string insertarSql = "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula) VALUES "+
                                //                     "('" + fecha.ToString().Substring(0,18) + "', '" + vehiculo +"', '" + txtregent.Text + "', '" + txttrans.Text + "', '" + txtrch.Text + "', " + suma_prod + ", '"+ unities + "', '" + productos + "', '" + txtobs.Text + "', '" + clasificacion + "', " + peso_entrada + ", "+ peso_bruto + ", " +tara + ", '" + txtregent.Text + "', 'P', '" + bas_clave + "') SELECT SCOPE_IDENTITY()";
                                cmnd1 = new SqlCommand(insertarSql, thisConnection);
                                //cmnd1.Parameters.AddWithValue("@aux_id_ticket", txtticket.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_fecha_entrada", fecha);
                                cmnd1.Parameters.AddWithValue("@aux_cargado", vehiculo.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_folio", numflete);
                                cmnd1.Parameters.AddWithValue("@aux_transportista", txttrans.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_rancho", txtrch.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_cantidad", suma_prod);
                                cmnd1.Parameters.AddWithValue("@aux_unidad", unities.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_producto", productos.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_observacion", txtobs.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_clasificacion", clasificacion.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_peso_entrada", peso_entrada);
                                cmnd1.Parameters.AddWithValue("@aux_peso_bruto", peso_bruto);
                                cmnd1.Parameters.AddWithValue("@aux_tara", tara);
                                cmnd1.Parameters.AddWithValue("@aux_peso_neto", peso_neto);
                                cmnd1.Parameters.AddWithValue("@id_entrada", txtregent.Text.Trim().Replace(", ", ""));
                                cmnd1.Parameters.AddWithValue("@aux_estatus", "P");
                                cmnd1.Parameters.AddWithValue("@id_bascula", bas_clave);
                                cmnd1.Parameters.AddWithValue("@hay_error", hay);
                                cmnd1.Parameters.AddWithValue("@error_comenta", txtIndError.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@prove_clave", prov_clave);
                                cmnd1.Parameters.AddWithValue("@hor_pes", txtpesohora.Value.ToString("HH:mm:ss"));
                                //reader1 = cmnd1.ExecuteReader();
                                //id_ticket = Convert.ToInt32(txtticket.Text);
                                id_ticket = Convert.ToInt32(cmnd1.ExecuteScalar());
                                //id_ticket = 1;
                                reader1.Dispose();

                                thisConnection.Close();
                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet QUERY 1 (un solo registro de báscula con más de un flete)", "SIPGAB");
                                thisConnection.Open();


                                //inserta en tb_det_recepcion_bascula
                                for (int i = 0; i < DGV1.Rows.Count; i++)
                                {
                                    string rc = Convert.ToString(DGV1.Rows[i].Cells["ranch"].Value).Trim();
                                    string[] rch = rc.Split('-');
                                    foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                                    {
                                        rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                                    }

                                    cmnd1 = thisConnection.CreateCommand();
                                    cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, peso_bruto, tara, " +
                                                      "peso_neto, id_tabla, tabla, num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus, flete, prov_clave, rch_clave, " +
                                                      "num_prod, rpt_recibo) values " +
                                                      "(" + id_ticket + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value).Trim() + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value).Trim() + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                                                      "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value).Trim() + "', " +
                                                      "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells["vari"].Value).Trim() + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value).Trim() + "', 'P', " + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + ", " +
                                                      "'" + prov_clave + "', '" + rch_clave + "', " + Convert.ToInt32((i + 1)) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells["rpt_rec"].Value).Trim() + "')";
                                    reader1 = cmnd1.ExecuteReader();
                                    reader1.Dispose();
                                }
                                thisConnection.Close();

                                thisConnection.Close();
                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet DETALLE QUERY 2 (un solo registro de báscula con más de un flete)", "SIPGAB");
                                thisConnection.Open();
                            }
                            catch (SqlException ex)
                            {
                                thisConnection.Close();
                                Utilerias.Class1.registro_errores(DateTime.Now, Utilerias.Class1.Usu_login, Environment.MachineName, "10.1", ex.ToString().Trim(), "MPSQL");
                                Utilerias.Class1.SendMail("jbravo@mrlucky.com.mx", "jbravo", "juanjose", ex.ToString().Trim());
                                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            for (int i = 0; i < DGV1.Rows.Count; i++)
                            {
                                // FIX BUG-01: Blindar contra 'connection already open'
                                // Si la conexión mySqlConn quedó abierta por un fallo previo,
                                // mySqlConn.Open() truena y se aborta la liberación del flete.
                                if (mySqlConn.State == System.Data.ConnectionState.Open) mySqlConn.Close();
                                try
                                {
                                    mySqlConn.Open();
                                    cmnd = mySqlConn.CreateCommand();
                                    //cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + fecha.ToString("s") + "', ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' where id_flete = " + numero;
                                    cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + txtpesohora.Value.ToString("s") + "', ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' where id_flete = " + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + " " +
                                                       "and id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                                    reader = cmnd.ExecuteReader();
                                    reader.Dispose();

                                    //for (int i = 0; i < DGV1.Rows.Count; i++)
                                    //{
                                    peso_bruto = Convert.ToDouble(DGV1.Rows[i].Cells[5].Value);
                                    tara = Convert.ToDouble(DGV1.Rows[i].Cells[6].Value);
                                    cmnd = mySqlConn.CreateCommand();
                                    cmnd.CommandText = "update tb_det_flete set tara = " + tara + ", peso_bruto = " + peso_bruto + ", " +
                                                       "caj_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[11].Value) + ", " +
                                                       "num_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[10].Value) + ", " +
                                                       "det_observa = '" + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + "' " +
                                                       "where id_producto = '" + Convert.ToString(DGV1.Rows[i].Cells[16].Value) + "' and " +
                                                       "id_flete = " + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + " and " +
                                                       "id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                                    reader = cmnd.ExecuteReader();
                                    reader.Dispose();
                                    //}




                                    cmnd = mySqlConn.CreateCommand();
                                    cmnd.CommandText = "insert into tb_mstr_bascula (id_flete, enviado, fecha_envio, hora_envio, fecha_recepcion, recibio, ticket_bascula, observacion, " +
                                                        "lugar, error, error_comenta) values " +
                                                    "(" + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + ", '" + txtrch.Text + "', '" + fecha.ToString("yyyy-MM-dd") + "', '" + fecha.ToShortTimeString() + "', '" + fecha.ToString("s") + "', '" + CBRecBas.SelectedItem.ToString() + "', " + txtticket.Text + ", '" + txtobs.Text + "', 'BASG', '" + hay + "', '" + txtIndError.Text + "')";
                                    reader = cmnd.ExecuteReader();
                                    reader.Dispose();
                                    mySqlConn.Close();



                                    lbindrev.Visible = false;
                                    lbIndError.Visible = false;
                                    CBIndRev.Visible = false;
                                    CBIndRev.SelectedIndex = 0;
                                    txtIndError.Visible = false;
                                }
                                catch (MySqlException ex)
                                {
                                    mySqlConn.Close();
                                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }//for
                            Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet QUERY 3 MOVIMIENTOS DE MYSQL (un solo registro de báscula con más de un flete)", "SIPGAB");
                        }
                        #endregion

                        #region un solo registro de báscula con un solo flete
                        else
                        {
                            try
                            {
                                fecha = DateTime.Now;

                                thisConnection.Open();
                                string insertarSql = "DECLARE @Error INT BEGIN TRAN " +
                                                     "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, " +
                                                     "clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula, error, error_comenta, prov_clave, " +
                                                     "hora_peso) VALUES (@aux_fecha_entrada, @aux_cargado, @aux_folio, @aux_transportista, @aux_rancho, @aux_cantidad, @aux_unidad, " +
                                                     "@aux_producto, @aux_observacion, @aux_clasificacion, @aux_peso_entrada, @aux_peso_bruto, @aux_tara, @aux_peso_neto, @id_entrada, " +
                                                     "@aux_estatus, @id_bascula, @hay_error, @error_comenta, @prove_clave, @hor_pes) SELECT SCOPE_IDENTITY() " +
                                                     "SET @Error = @@ERROR IF( @Error <> 0) GOTO TratarError COMMIT TRAN TratarError: " +
                                                     "IF @@Error<>0 BEGIN PRINT 'ha ocurrido un error' ROLLBACK TRAN END";
                                //string insertarSql = "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula) VALUES "+
                                //                     "('" + fecha.ToString().Substring(0,18) + "', '" + vehiculo +"', '" + txtregent.Text + "', '" + txttrans.Text + "', '" + txtrch.Text + "', " + suma_prod + ", '"+ unities + "', '" + productos + "', '" + txtobs.Text + "', '" + clasificacion + "', " + peso_entrada + ", "+ peso_bruto + ", " +tara + ", '" + txtregent.Text + "', 'P', '" + bas_clave + "') SELECT SCOPE_IDENTITY()";
                                cmnd1 = new SqlCommand(insertarSql, thisConnection);
                                //cmnd1.Parameters.AddWithValue("@aux_id_ticket", txtticket.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_fecha_entrada", fecha);
                                cmnd1.Parameters.AddWithValue("@aux_cargado", vehiculo.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_folio", numflete.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_transportista", txttrans.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_rancho", txtrch.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_cantidad", suma_prod);
                                cmnd1.Parameters.AddWithValue("@aux_unidad", unities.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_producto", productos.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_observacion", txtobs.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_clasificacion", clasificacion.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_peso_entrada", peso_entrada);
                                cmnd1.Parameters.AddWithValue("@aux_peso_bruto", peso_bruto);
                                cmnd1.Parameters.AddWithValue("@aux_tara", tara);
                                cmnd1.Parameters.AddWithValue("@aux_peso_neto", peso_neto);
                                cmnd1.Parameters.AddWithValue("@id_entrada", txtregent.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_estatus", "P");
                                cmnd1.Parameters.AddWithValue("@id_bascula", bas_clave);
                                cmnd1.Parameters.AddWithValue("@hay_error", hay);
                                cmnd1.Parameters.AddWithValue("@error_comenta", txtIndError.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@prove_clave", prov_clave);
                                cmnd1.Parameters.AddWithValue("@hor_pes", txtpesohora.Value.ToString("HH:mm:ss"));
                                //reader1 = cmnd1.ExecuteReader();
                                //id_ticket = Convert.ToInt32(txtticket.Text);
                                id_ticket = Convert.ToInt32(cmnd1.ExecuteScalar());
                                //id_ticket = 1;
                                reader1.Dispose();

                                thisConnection.Close();
                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet QUERY 5 (un solo registro de báscula con un solo flete)", "SIPGAB");
                                thisConnection.Open();

                                //inserta en tb_det_recepcion_bascula
                                for (int i = 0; i < DGV1.Rows.Count; i++)
                                {
                                    string rc = Convert.ToString(DGV1.Rows[i].Cells["ranch"].Value).Trim();
                                    string[] rch = rc.Split('-');
                                    foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                                    {
                                        rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                                    }
                                    cmnd1 = thisConnection.CreateCommand();
                                    cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, peso_bruto , tara, " +
                                                      "peso_neto, id_tabla, tabla, num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus, prov_clave, rch_clave, " +
                                                      "num_prod, flete, rpt_recibo) values " +
                                                      "(" + id_ticket + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value).Trim() + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value).Trim() + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                                                      "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value).Trim() + "', " +
                                                      "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells["vari"].Value).Trim() + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value).Trim() + "', 'P', '" + prov_clave + "', '" + rch_clave + "', " + Convert.ToInt32((i + 1)) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[17].Value) + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells["rpt_rec"].Value).Trim() + "')";
                                    reader1 = cmnd1.ExecuteReader();
                                    reader1.Dispose();
                                }
                                thisConnection.Close();

                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet QUERY 6 DET_RECEPCION_BASCULA(un solo registro de báscula con un solo flete)", "SIPGAB");
                            }
                            catch (SqlException ex)
                            {
                                thisConnection.Close();
                                Utilerias.Class1.registro_errores(DateTime.Now, Utilerias.Class1.Usu_login, Environment.MachineName, "10.1", ex.ToString().Trim(), "MPSQL");
                                Utilerias.Class1.SendMail("jbravo@mrlucky.com.mx", "jbravo", "juanjose", ex.ToString().Trim());
                                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // FIX BUG-01: Blindar contra 'connection already open'
                            if (mySqlConn.State == System.Data.ConnectionState.Open) mySqlConn.Close();
                            try
                            {
                                mySqlConn.Open();
                                cmnd = mySqlConn.CreateCommand();
                                //cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + fecha.ToString("s") + "', ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' where id_flete = " + numero;
                                cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + txtpesohora.Value.ToString("s") + "', ticket_bascula = '" + txtticket.Text + "', " +
                                                   "recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', comenta_bascula = '" + txtobs.Text + "', " +
                                                   "hora_recepcion = '" + fecha.ToShortTimeString() + "' where id_flete = " + numero + " and id_proveedor = '" + txtclaveprov.Text + "'";
                                reader = cmnd.ExecuteReader();
                                reader.Dispose();

                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet QUERY 6 ACTUALIZACION MYSQL FLETE(un solo registro de báscula con un solo flete)", "SIPGAB");

                                for (int i = 0; i < DGV1.Rows.Count; i++)
                                {
                                    peso_bruto = Convert.ToDouble(DGV1.Rows[i].Cells[5].Value);
                                    tara = Convert.ToDouble(DGV1.Rows[i].Cells[6].Value);
                                    cmnd = mySqlConn.CreateCommand();
                                    cmnd.CommandText = "update tb_det_flete set tara = " + tara + ", peso_bruto = " + peso_bruto + ", " +
                                                       "caj_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[11].Value) + ", " +
                                                       "num_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[10].Value) + ", " +
                                                       "det_observa = '" + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + "' " +
                                                       "where id_producto = '" + Convert.ToString(DGV1.Rows[i].Cells[16].Value) + "' and id_flete = " + numero + " " +
                                                       "and id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                                    reader = cmnd.ExecuteReader();
                                    reader.Dispose();
                                }

                                cmnd = mySqlConn.CreateCommand();
                                cmnd.CommandText = "insert into tb_mstr_bascula (id_flete, enviado, fecha_envio, hora_envio, fecha_recepcion, recibio, ticket_bascula, observacion, lugar, error, error_comenta) values " +
                                                "(" + numero + ", '" + txtrch.Text + "', '" + fecha.ToString("yyyy-MM-dd") + "', '" + fecha.ToShortTimeString() + "', '" + fecha.ToString("s") + "', '" + CBRecBas.SelectedItem.ToString() + "', " + txtticket.Text + ", '" + txtobs.Text + "', 'BASG', '" + hay + "', '" + txtIndError.Text + "')";
                                reader = cmnd.ExecuteReader();
                                reader.Dispose();
                                mySqlConn.Close();

                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de Internet QUERY 6 ACTUALIZACION MYSQL MSTR BASCULA (un solo registro de báscula con un solo flete)", "SIPGAB");
                                lbindrev.Visible = false;
                                lbIndError.Visible = false;
                                CBIndRev.Visible = false;
                                CBIndRev.SelectedIndex = 0;
                                txtIndError.Visible = false;

                            }
                            catch (MySqlException ex)
                            {
                                mySqlConn.Close();
                                Utilerias.Class1.registro_errores(DateTime.Now, Utilerias.Class1.Usu_login, Environment.MachineName, "10.1", ex.ToString().Trim(), "MPSQL");
                                Utilerias.Class1.SendMail("jbravo@mrlucky.com.mx", "jbravo", "juanjose", ex.ToString().Trim());
                                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region si el flete es de vigilancia
                    if ((aux_opcion == "V") || (aux_opcion.Trim() == ""))
                    {
                        #region cuando es un solo flete
                        if (CBmasFletes.Checked == false)
                        {
                            try
                            {
                                fecha = DateTime.Now;
                                productos = productos.Replace("'", "''");
                                if (cont > 1)
                                {
                                    txtregent.Text = " ";
                                    if (txttrans.Text.Length > 99)
                                        txttrans.Text = txttrans.Text.Substring(0, 90);
                                }

                                thisConnection.Open();
                                string insertarSql = "DECLARE @Error INT BEGIN TRAN " +
                                                     "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, " +
                                                     "clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula, error, error_comenta, prov_clave, " +
                                                     "hora_peso) VALUES (@aux_fecha_entrada, @aux_cargado, @aux_folio, @aux_transportista, @aux_rancho, @aux_cantidad, @aux_unidad, " +
                                                     "@aux_producto, @aux_observacion, @aux_clasificacion, @aux_peso_entrada, @aux_peso_bruto, @aux_tara, @aux_peso_neto, @id_entrada, " +
                                                     "@aux_estatus, @id_bascula, @hay_error, @error_comenta, @prove_clave, @hor_pes) SELECT SCOPE_IDENTITY() " +
                                                     "SET @Error = @@ERROR IF( @Error <> 0) GOTO TratarError COMMIT TRAN TratarError: " +
                                                     "IF @@Error<>0 BEGIN PRINT 'ha ocurrido un error' ROLLBACK TRAN END";
                                //string insertarSql = "DECLARE @Error INT BEGIN TRAN INSERT INTO tb_mstr_recepcion_bascula (id_ticket,fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula, error, error_comenta) VALUES (@aux_id_ticket, @aux_fecha_entrada, @aux_cargado, @aux_folio, @aux_transportista, @aux_rancho, @aux_cantidad, @aux_unidad, @aux_producto, @aux_observacion, @aux_clasificacion, @aux_peso_entrada, @aux_peso_bruto, @aux_tara, @aux_peso_neto, @id_entrada, @aux_estatus, @id_bascula, @error, @error_comenta) SELECT SCOPE_IDENTITY() AS id_ticket " +
                                //                     "SET @Error = @@ERROR IF( @Error <> 0) GOTO TratarError COMMIT TRAN TratarError: " +
                                //                     "IF @@Error<>0 BEGIN PRINT 'ha ocurrido un error' ROLLBACK TRAN END";
                                cmnd1 = new SqlCommand(insertarSql, thisConnection);
                                //cmnd1.Parameters.AddWithValue("@aux_id_ticket", txtticket.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_fecha_entrada", fecha);
                                cmnd1.Parameters.AddWithValue("@aux_cargado", vehiculo);
                                cmnd1.Parameters.AddWithValue("@aux_folio", txtregent.Text);
                                cmnd1.Parameters.AddWithValue("@aux_transportista", txttrans.Text);
                                cmnd1.Parameters.AddWithValue("@aux_rancho", txtrch.Text);
                                cmnd1.Parameters.AddWithValue("@aux_cantidad", suma_prod);
                                cmnd1.Parameters.AddWithValue("@aux_unidad", unities);
                                cmnd1.Parameters.AddWithValue("@aux_producto", productos);
                                cmnd1.Parameters.AddWithValue("@aux_observacion", txtobs.Text);
                                cmnd1.Parameters.AddWithValue("@aux_clasificacion", clasificacion);
                                cmnd1.Parameters.AddWithValue("@aux_peso_entrada", peso_entrada);
                                cmnd1.Parameters.AddWithValue("@aux_peso_bruto", peso_bruto);
                                cmnd1.Parameters.AddWithValue("@aux_tara", tara);
                                cmnd1.Parameters.AddWithValue("@aux_peso_neto", peso_neto);
                                cmnd1.Parameters.AddWithValue("@id_entrada", txtregent.Text.Replace(",", ""));
                                cmnd1.Parameters.AddWithValue("@aux_estatus", "P");
                                cmnd1.Parameters.AddWithValue("@id_bascula", bas_clave);
                                cmnd1.Parameters.AddWithValue("@hay_error", hay);
                                cmnd1.Parameters.AddWithValue("@error_comenta", txtIndError.Text);
                                cmnd1.Parameters.AddWithValue("@prove_clave", prov_clave);
                                cmnd1.Parameters.AddWithValue("@hor_pes", txtpesohora.Value.ToString("HH:mm:ss"));
                                id_ticket = Convert.ToInt32(cmnd1.ExecuteScalar());
                                //reader1 = cmnd1.ExecuteReader();
                                //id_ticket = Convert.ToInt32(txtticket.Text);
                                reader1.Dispose();

                                thisConnection.Close();
                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia QUERY 1 (cuando es un solo flete)", "SIPGAB");
                                thisConnection.Open();

                                //inserta en tb_det_recepcion_bascula
                                #region
                                //for (int i = 0; i < DGV1.Rows.Count; i++)
                                //{
                                //    cmnd1 = thisConnection.CreateCommand();
                                //    cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, tara, peso_bruto, " +
                                //                      "peso_neto, id_tabla, tabla, " +
                                //                      "num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus) values " +
                                //                      "(" + id_ticket + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value).Trim() + "', " +
                                //                      "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                                //                      "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value).Trim() + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                                //                      "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                                //                      "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value).Trim() + "', " +
                                //                      "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                                //                      "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells[13].Value).Trim() + "', " +
                                //                      "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value).Trim() + "', 'P')";
                                //    reader1 = cmnd1.ExecuteReader();
                                //    reader1.Dispose();
                                //}
                                #endregion

                                if (DGV1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < DGV1.Rows.Count; i++)
                                    {
                                        string rc = Convert.ToString(DGV1.Rows[i].Cells["ranch"].Value).Trim();
                                        string[] rch = rc.Split('-');
                                        foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                                        {
                                            rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                                        }
                                        cmnd1 = thisConnection.CreateCommand();
                                        cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, peso_bruto, tara, " +
                                                          "peso_neto, id_tabla, tabla, num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus, prov_clave, rch_clave, " +
                                                          "num_prod, flete, rpt_recibo) values " +
                                                          "(" + id_ticket + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "', " +
                                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value).Trim() + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                                                          "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value).Trim() + "', " +
                                                          "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells["Column17"].Value).Trim() + "', " +
                                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value).Trim() + "', 'P', '" + prov_clave + "', '" + rch_clave + "', " +
                                                          "" + Convert.ToInt32((i + 1)) + ", '" + Convert.ToString(DGV1.Rows[i].Cells[17].Value) + "', " +
                                                          "'" + Convert.ToString(DGV1.Rows[i].Cells["rpt_rec"].Value).Trim() + "')";
                                        reader1 = cmnd1.ExecuteReader();
                                        reader1.Dispose();
                                    }
                                }

                                thisConnection.Close();
                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia DETALLE QUERY 2 (cuando es un solo flete)", "SIPGAB");
                                thisConnection.Open();

                                cmnd1 = thisConnection.CreateCommand();
                                cmnd1.CommandText = "SELECT COUNT(*) FROM tb_mstr_registros_vigilancia WHERE id_entrada = " + Convert.ToInt32(txtregent.Text);
                                int existe = Convert.ToInt32(cmnd1.ExecuteScalar());
                                reader1.Dispose();

                                if (existe > 0)
                                {
                                    cmnd1 = thisConnection.CreateCommand();
                                    cmnd1.CommandText = "UPDATE tb_mstr_registros_vigilancia SET registro_bascula = 'S' WHERE id_entrada = " + Convert.ToInt32(txtregent.Text);
                                    reader1 = cmnd1.ExecuteReader();
                                    reader1.Dispose();
                                }
                                thisConnection.Close();
                            }
                            catch (SqlException ex)
                            {
                                thisConnection.Close();
                                Utilerias.Class1.registro_errores(DateTime.Now, Utilerias.Class1.Usu_login, Environment.MachineName, "10.1", ex.ToString().Trim(), "MPSQL");
                                Utilerias.Class1.SendMail("jbravo@mrlucky.com.mx", "jbravo", "juanjose", ex.ToString().Trim());
                                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (numflete.Trim() != "")
                            {
                                // FIX BUG-01: Blindar contra 'connection already open'
                                if (mySqlConn.State == System.Data.ConnectionState.Open) mySqlConn.Close();
                                try
                                {
                                    mySqlConn.Open();
                                    cmnd = mySqlConn.CreateCommand();
                                    //cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + fecha.ToString("s") + "', ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' where id_flete = " + numero;
                                    cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + txtpesohora.Value.ToString("s") + "', " +
                                                       "ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', " +
                                                       "comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' " +
                                                       "where id_flete = " + numero + " and id_proveedor = '" + txtclaveprov.Text + "'";
                                    reader = cmnd.ExecuteReader();
                                    reader.Dispose();

                                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia QUERY 3 ACTUALIZACION DE MYSQL FLETE (cuando es un solo flete)", "SIPGAB");


                                    for (int i = 0; i < DGV1.Rows.Count; i++)
                                    {
                                        peso_bruto = Convert.ToDouble(DGV1.Rows[i].Cells[5].Value);
                                        tara = Convert.ToDouble(DGV1.Rows[i].Cells[6].Value);
                                        cmnd = mySqlConn.CreateCommand();
                                        cmnd.CommandText = "update tb_det_flete set tara = " + tara + ", peso_bruto = " + peso_bruto + ", " +
                                                           "caj_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[11].Value) + ", " +
                                                           "num_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[10].Value) + ", " +
                                                           "det_observa = '" + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + "' " +
                                                           "where id_producto = '" + Convert.ToString(DGV1.Rows[i].Cells["prod_ofc"].Value) + "' " +
                                                           "and id_flete = " + numero + " and id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                                        reader = cmnd.ExecuteReader();
                                        reader.Dispose();
                                    }

                                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia QUERY 4 ACTUALIZACION DE MYSQL DETALLE FLETE (cuando es un solo flete)", "SIPGAB");

                                    cmnd = mySqlConn.CreateCommand();
                                    cmnd.CommandText = "insert into tb_mstr_bascula (id_flete, enviado, fecha_envio, hora_envio, fecha_recepcion, recibio, ticket_bascula, observacion, lugar, error, error_comenta) values " +
                                                    "(" + numero + ", '" + txtrch.Text + "', '" + fecha.ToString("yyyy-MM-dd") + "', '" + fecha.ToShortTimeString() + "', '" + fecha.ToString("s") + "', '" + CBRecBas.SelectedItem.ToString() + "', " + txtticket.Text + ", '" + txtobs.Text + "', 'BASG', '" + hay + "', '" + txtIndError.Text + "')";
                                    reader = cmnd.ExecuteReader();
                                    reader.Dispose();
                                    mySqlConn.Close();

                                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia QUERY 5 INSERCION DE MYSQL MSTR_BASCULA (cuando es un solo flete)", "SIPGAB");

                                    lbindrev.Visible = false;
                                    lbIndError.Visible = false;
                                    CBIndRev.Visible = false;
                                    CBIndRev.SelectedIndex = 0;
                                    txtIndError.Visible = false;

                                }
                                catch (MySqlException ex)
                                {
                                    mySqlConn.Close();
                                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        #endregion

                        #region un solo registro de báscula con más de un flete
                        if (CBmasFletes.Checked == true)
                        {
                            try
                            {
                                if (cont > 1)
                                {
                                    txtregent.Text = " ";
                                    txttrans.Text = "";
                                }
                                fecha = DateTime.Now;
                                txtregent.Text = "";
                                thisConnection.Open();
                                string insertarSql = "DECLARE @Error INT BEGIN TRAN " +
                                                     "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, " +
                                                     "clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula, error, error_comenta, prov_clave, " +
                                                     "hora_peso) VALUES (@aux_fecha_entrada, @aux_cargado, @aux_folio, @aux_transportista, @aux_rancho, @aux_cantidad, " +
                                                     "@aux_unidad, @aux_producto, @aux_observacion, @aux_clasificacion, @aux_peso_entrada, @aux_peso_bruto, @aux_tara, @aux_peso_neto, " +
                                                     "@id_entrada, @aux_estatus, @id_bascula, @hay_error, @error_comenta, @prove_clave, @hor_pes) SELECT SCOPE_IDENTITY() " +
                                                     "SET @Error = @@ERROR IF( @Error <> 0) GOTO TratarError COMMIT TRAN TratarError: " +
                                                     "IF @@Error<>0 BEGIN PRINT 'ha ocurrido un error' ROLLBACK TRAN END";
                                //string insertarSql = "INSERT INTO tb_mstr_recepcion_bascula (fecha_entrada, cargado, folio, transportista, rancho, cantidad, unidad, producto, observacion, clasificacion, peso_entrada, peso_bruto, tara, peso_neto, id_entrada, estatus, recibio_bascula) VALUES "+
                                //                     "('" + fecha.ToString().Substring(0,18) + "', '" + vehiculo +"', '" + txtregent.Text + "', '" + txttrans.Text + "', '" + txtrch.Text + "', " + suma_prod + ", '"+ unities + "', '" + productos + "', '" + txtobs.Text + "', '" + clasificacion + "', " + peso_entrada + ", "+ peso_bruto + ", " +tara + ", '" + txtregent.Text + "', 'P', '" + bas_clave + "') SELECT SCOPE_IDENTITY()";
                                cmnd1 = new SqlCommand(insertarSql, thisConnection);
                                //cmnd1.Parameters.AddWithValue("@aux_id_ticket", txtticket.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_fecha_entrada", fecha);
                                cmnd1.Parameters.AddWithValue("@aux_cargado", vehiculo.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_folio", txtregent.Text);
                                cmnd1.Parameters.AddWithValue("@aux_transportista", txttrans.Text);
                                cmnd1.Parameters.AddWithValue("@aux_rancho", txtrch.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_cantidad", suma_prod);
                                cmnd1.Parameters.AddWithValue("@aux_unidad", unities.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_producto", productos.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_observacion", txtobs.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_clasificacion", clasificacion.Trim());
                                cmnd1.Parameters.AddWithValue("@aux_peso_entrada", peso_entrada);
                                cmnd1.Parameters.AddWithValue("@aux_peso_bruto", peso_bruto);
                                cmnd1.Parameters.AddWithValue("@aux_tara", tara);
                                cmnd1.Parameters.AddWithValue("@aux_peso_neto", peso_neto);
                                cmnd1.Parameters.AddWithValue("@id_entrada", txtregent.Text);
                                cmnd1.Parameters.AddWithValue("@aux_estatus", "P");
                                cmnd1.Parameters.AddWithValue("@id_bascula", bas_clave);
                                cmnd1.Parameters.AddWithValue("@hay_error", hay);
                                cmnd1.Parameters.AddWithValue("@error_comenta", txtIndError.Text.Trim());
                                cmnd1.Parameters.AddWithValue("@prove_clave", prov_clave);
                                cmnd1.Parameters.AddWithValue("@hor_pes", txtpesohora.Value.ToString("HH:mm:ss"));
                                //reader1 = cmnd1.ExecuteReader();
                                //id_ticket = Convert.ToInt32(txtticket.Text);
                                id_ticket = Convert.ToInt32(cmnd1.ExecuteScalar());
                                //id_ticket = 1;
                                reader1.Dispose();

                                thisConnection.Close();
                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia QUERY 1 (un solo registro de báscula con más de un flete)", "SIPGAB");
                                thisConnection.Open();

                                //inserta en tb_det_recepcion_bascula
                                for (int i = 0; i < DGV1.Rows.Count; i++)
                                {
                                    string rc = Convert.ToString(DGV1.Rows[i].Cells["ranch"].Value).Trim();
                                    string[] rch = rc.Split('-');
                                    foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                                    {
                                        rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                                    }
                                    cmnd1 = thisConnection.CreateCommand();
                                    cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, peso_bruto, tara, " +
                                                      "peso_neto, id_tabla, tabla, num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus, prov_clave, rch_clave, " +
                                                      "num_prod, flete, rpt_recibo) values " +
                                                      "(" + id_ticket + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value).Trim() + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                                                      "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value).Trim() + "', " +
                                                      "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value).Trim() + "', '" + Convert.ToString(DGV1.Rows[i].Cells["Column17"].Value).Trim() + "', " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value).Trim() + "', 'P', '" + prov_clave + "', '" + rch_clave + "', " +
                                                      "" + Convert.ToInt32((i + 1)) + ", " + Convert.ToString(DGV1.Rows[i].Cells[17].Value) + ", " +
                                                      "'" + Convert.ToString(DGV1.Rows[i].Cells["rpt_rec"].Value).Trim() + "')";
                                    reader1 = cmnd1.ExecuteReader();
                                    reader1.Dispose();
                                }
                                thisConnection.Close();

                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia DETALLE QUERY 2 (un solo registro de báscula con más de un flete)", "SIPGAB");

                            }
                            catch (SqlException ex)
                            {
                                thisConnection.Close();
                                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (numflete.Trim() != "")
                            {
                                for (int i = 0; i < DGV1.Rows.Count; i++)
                                {
                                    // FIX BUG-01: Blindar contra 'connection already open'
                                    if (mySqlConn.State == System.Data.ConnectionState.Open) mySqlConn.Close();
                                    try
                                    {
                                        mySqlConn.Open();
                                        cmnd = mySqlConn.CreateCommand();
                                        //cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + fecha.ToString("s") + "', ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' where id_flete = " + numero;
                                        cmnd.CommandText = "update tb_mstr_flete set estatus = 'R', fecha_recepcion = '" + txtpesohora.Value.ToString("s") + "', " +
                                                           "ticket_bascula = '" + txtticket.Text + "', recibio_bascula = '" + CBRecBas.SelectedItem.ToString() + "', " +
                                                           "comenta_bascula = '" + txtobs.Text + "', hora_recepcion = '" + fecha.ToShortTimeString() + "' " +
                                                           "where id_flete = " + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + " and " +
                                                           "id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                                        reader = cmnd.ExecuteReader();
                                        reader.Dispose();


                                        //for (int i = 0; i < DGV1.Rows.Count; i++)
                                        //{
                                        peso_bruto = Convert.ToDouble(DGV1.Rows[i].Cells[5].Value);
                                        tara = Convert.ToDouble(DGV1.Rows[i].Cells[6].Value);
                                        cmnd = mySqlConn.CreateCommand();
                                        cmnd.CommandText = "update tb_det_flete set tara = " + tara + ", peso_bruto = " + peso_bruto + ", " +
                                                           "caj_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[11].Value) + ", " +
                                                           "num_tarimas = " + Convert.ToDouble(DGV1.Rows[i].Cells[10].Value) + ", " +
                                                           "det_observa = '" + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + "' " +
                                                           "where id_producto = '" + Convert.ToString(DGV1.Rows[i].Cells[16].Value) + "' " +
                                                           "and id_flete = " + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + " " +
                                                           "and id_proveedor = '" + Convert.ToString(DGV1.Rows[i].Cells["prove"].Value) + "'";
                                        reader = cmnd.ExecuteReader();
                                        reader.Dispose();
                                        //}

                                        cmnd = mySqlConn.CreateCommand();
                                        cmnd.CommandText = "insert into tb_mstr_bascula (id_flete, enviado, fecha_envio, hora_envio, fecha_recepcion, recibio, ticket_bascula, observacion, lugar, error, error_comenta) values " +
                                                        "(" + Convert.ToInt32(DGV1.Rows[i].Cells["folio"].Value) + ", '" + txtrch.Text + "', '" + fecha.ToString("yyyy-MM-dd") + "', '" + fecha.ToShortTimeString() + "', '" + fecha.ToString("s") + "', '" + CBRecBas.SelectedItem.ToString() + "', " + txtticket.Text + ", '" + txtobs.Text + "', 'BASG', '" + hay + "', '" + txtIndError.Text + "')";
                                        reader = cmnd.ExecuteReader();
                                        reader.Dispose();
                                        mySqlConn.Close();
                                        lbindrev.Visible = false;
                                        lbIndError.Visible = false;
                                        CBIndRev.Visible = false;
                                        CBIndRev.SelectedIndex = 0;
                                        txtIndError.Visible = false;

                                    }
                                    catch (MySqlException ex)
                                    {
                                        mySqlConn.Close();
                                        Utilerias.Class1.registro_errores(DateTime.Now, Utilerias.Class1.Usu_login, Environment.MachineName, "10.1", ex.ToString().Trim(), "MPSQL");
                                        Utilerias.Class1.SendMail("jbravo@mrlucky.com.mx", "jbravo", "juanjose", ex.ToString().Trim());
                                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }//for

                                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "V", "", id_ticket.ToString(), "si el flete es de vigilancia QUERY 3 MOVIMIENTOS A MYSQL (un solo registro de báscula con más de un flete)", "SIPGAB");
                            }
                        }
                        #endregion
                    }
                    #endregion

                    // FIX BUG-02: Recargar lista de fletes para que el liberado desaparezca de la UI.
                    // Si falla, el operador puede refrescar manualmente con el botón ALTA.
                    try
                    {
                        listBox1.Items.Clear();
                        cargar_fletes_pendientes_internet();
                    }
                    catch { /* no es crítico */ }

                    MessageBox.Show("El registro ha sido guardado con éxito", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //if (txtlin_clave.Text.Trim() == "08")
                    //{
                    //    #region guarda en tb_mstr_recepcion_esparrago para saber el número de recibo que le corresponde
                    //    thisConnection.Open();
                    //    cmnd1 = thisConnection.CreateCommand();
                    //    cmnd1.CommandText = "insert into tb_mstr_recepcion_esparrago (rmp_fecha, rmp_ticket, prov_clave, rch_clave, tbl_clave, prod_clave, lin_clave, rmp_pesador, rmp_hora)  " +
                    //                        "values ('" + DTPFecha.Value.ToShortDateString() + "', '" + txtticket.Text + "', '" + txtclaveprov.Text + "', '" + rch_clave + "', " +
                    //                        "'" + tabl + "', '08008', '08', '" + CBRecBas.SelectedItem.ToString().Trim() + "', '" + txtpesohora.Text + "') select scope_identity()";
                    //    rec_esp = Convert.ToString(cmnd1.ExecuteScalar()).Trim();
                    //    thisConnection.Close();
                    //    #endregion

                    //    PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                    //    pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 307, 393);
                    //    pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                    //    //PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
                    //    //VistaPrevia.Document = pd;
                    //    //VistaPrevia.ShowDialog();
                    //    pd.Print();                            
                    //}

                    for (int i = 0; i < DGV1.Rows.Count; i++)
                    {
                        if ((Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() == "08") || (Convert.ToString(DGV1.Rows[i].Cells[2].Value).Trim().Contains("ESPARRAGO")))
                        {
                            #region guarda en tb_mstr_recepcion_esparrago para saber el número de recibo que le corresponde
                            canti = 0;
                            canti = Convert.ToDecimal(DGV1.Rows[i].Cells["Columna02"].Value);
                            thisConnection.Open();
                            cmnd1 = thisConnection.CreateCommand();
                            cmnd1.CommandText = "insert into tb_mstr_recepcion_esparrago (rmp_fecha, rmp_ticket, prov_clave, rch_clave, tbl_clave, prod_clave, lin_clave, rmp_pesador, rmp_hora, rmp_num_envase, rmp_estatus) " +
                                                "values ('" + DTPFecha.Value.ToShortDateString() + "', '" + txtticket.Text + "', '" + txtclaveprov.Text + "', '" + rch_clave + "', " +
                                                "'" + tabl + "', '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "', '" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "', " +
                                                "'" + CBRecBas.SelectedItem.ToString().Trim() + "', '" + txtpesohora.Value.ToString("HH:mm:ss") + "', " + canti + ", 'P') " +
                                                "select scope_identity()";
                            rec_esp = Convert.ToString(cmnd1.ExecuteScalar()).Trim();

                            cmnd1 = thisConnection.CreateCommand();
                            cmnd1.CommandText = "update tb_mstr_recepcion_esparrago set rmp_estatus = 'P' where rmp_recibo = " + rec_esp + "";
                            reader1 = cmnd1.ExecuteReader();
                            reader1.Dispose();
                            thisConnection.Close();

                            Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "10.1", rec_esp, "REGISTRO DE NUEVO TICKET DE BASCULA " + txtticket.Text + " CON ESPARRAGO " + rec_esp, "SIPGAB");
                            #endregion

                            PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                            pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 307, 393);
                            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                            //PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
                            //VistaPrevia.Document = pd;
                            //VistaPrevia.ShowDialog();
                            pd.Print();
                        }
                    }
                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "10.1", txtticket.Text, "REGISTRO DE NUEVO TICKET DE BASCULA " + txtticket.Text, "SIPGAB");
                }
                #endregion

                #region guardar modificaciones
                if (opcion == 3)
                {
                    try
                    {
                        productos = productos.Replace("'", "''");
                        thisConnection.Open();
                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "update tb_mstr_recepcion_bascula set cargado = '" + vehiculo + "', transportista = '" + txttrans.Text + "', rancho='" + txtrch.Text + "', " +
                                            "cantidad = " + suma_prod + ", unidad = '" + unities + "', producto = '" + productos + "', observacion = '" + txtobs.Text + "', " +
                                            "clasificacion = '" + clasificacion + "', peso_entrada = " + peso_entrada + ", peso_bruto = " + peso_bruto + ", tara = " + tara + ", " +
                                            "peso_neto = " + peso_neto + ", recibio_bascula = " + bas_clave + ", error = '" + hay + "', error_comenta = '" + txtIndError.Text + "', " +
                                            "prov_clave = '" + prov_clave + "', hora_peso = '" + txtpesohora.Value.ToString("HH:mm:ss") + "' where id_ticket = " + txtticket.Text;
                        reader1 = cmnd1.ExecuteReader();
                        reader1.Dispose();

                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "delete tb_det_recepcion_bascula where estatus = 'P' and id_ticket = " + txtticket.Text;
                        reader1 = cmnd1.ExecuteReader();
                        reader1.Dispose();

                        int numero_productos = DGV1.Rows.Count;
                        //inserta en tb_det_recepcion_bascula
                        for (int i = 0; i < numero_productos; i++)
                        {
                            if (Convert.ToString(DGV1.Rows[i].Cells[20].Value) != "R")
                            {
                                string rc = Convert.ToString(DGV1.Rows[i].Cells["ranch"].Value).Trim();
                                string[] rch = rc.Split('-');
                                foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                                {
                                    rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                                }

                                cmnd1 = thisConnection.CreateCommand();
                                cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, peso_bruto, tara, " +
                                          "peso_neto, id_tabla, tabla, num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus, prov_clave, rch_clave, " +
                                          "num_prod, rpt_recibo) values " +
                                          "(" + txtticket.Text + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "', " +
                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value) + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                                          "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value) + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value) + "', " +
                                          "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value) + "', '" + Convert.ToString(DGV1.Rows[i].Cells[13].Value).Trim() + "', " +
                                          "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + "', 'P', '" + prov_clave + "', '" + rch_clave + "', " +
                                          "" + Convert.ToInt32((i + 1)) + ", '" + Convert.ToString(DGV1.Rows[i].Cells["rpt_rec"].Value).Trim() + "')";
                                reader1 = cmnd1.ExecuteReader();
                                reader1.Dispose();
                            }
                            //while (reader11.Read())
                            //{
                            //    if (reader11.GetValue(0).ToString().Trim() != "R")
                            //    {
                            //        cmnd1 = thisConnection.CreateCommand();
                            //        cmnd1.CommandText = "insert into tb_det_recepcion_bascula (id_ticket, prod_clave, lin_clave, cantidad, envase, peso_bruto, tara, " +
                            //                  "peso_neto, id_tabla, tabla, " +
                            //                  "num_tarimas, caj_tarimas, tipo_prod, variedad, observaciones, estatus, prov_clave, rch_clave, num_prod) values " +
                            //                  "(" + txtticket.Text + ", '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "', " +
                            //                  "'" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "', " + Convert.ToInt32(DGV1.Rows[i].Cells[3].Value) + ", " +
                            //                  "'" + Convert.ToString(DGV1.Rows[i].Cells[4].Value) + "', " + Convert.ToDouble(DGV1.Rows[i].Cells[5].Value) + ", " +
                            //                  "" + Convert.ToDouble(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToDouble(DGV1.Rows[i].Cells[7].Value) + ", " +
                            //                  "'" + Convert.ToString(DGV1.Rows[i].Cells[8].Value) + "', '" + Convert.ToString(DGV1.Rows[i].Cells[9].Value) + "', " +
                            //                  "" + Convert.ToInt32(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToInt32(DGV1.Rows[i].Cells[11].Value) + " , " +
                            //                  "'" + Convert.ToString(DGV1.Rows[i].Cells[12].Value) + "', '" + Convert.ToString(DGV1.Rows[i].Cells[13].Value).Trim() + "', " +
                            //                  "'" + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + "', 'P', '" + prov_clave + "', '" + rch_clave + "', " + Convert.ToInt32((i + 1)) + ")";
                            //        reader1 = cmnd1.ExecuteReader();
                            //        reader1.Dispose();
                            //    }
                            //}
                        }

                        thisConnection.Close();
                        for (int i = 0; i < DGV1.Rows.Count; i++)
                        {
                            if ((Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() == "08") || (Convert.ToString(DGV1.Rows[i].Cells[2].Value).Trim().Contains("ESPARRAGO")))
                            {
                                if (Convert.ToString(DGV1.Rows[i].Cells[20].Value) != "R")
                                {
                                    #region guarda en tb_mstr_recepcion_esparrago para saber el número de recibo que le corresponde
                                    tabl = Convert.ToString(DGV1.Rows[i].Cells["Column11"].Value);
                                    canti = 0;
                                    canti = Convert.ToDecimal(DGV1.Rows[i].Cells["Columna02"].Value);
                                    thisConnection.Open();
                                    cmnd1 = thisConnection.CreateCommand();
                                    cmnd1.CommandText = "select rmp_recibo, rmp_estatus from tb_mstr_recepcion_esparrago where rmp_ticket = '" + txtticket.Text + "' and (rmp_num_envase = " + Convert.ToInt32(DGV1.Rows[i].Cells["prod_ofc"].Value) + " or rmp_num_envase is null)";
                                    reader1 = cmnd1.ExecuteReader();
                                    while (reader1.Read())
                                    {
                                        rec_esp = reader1.GetValue(0).ToString().Trim();
                                        esttus = reader1.GetValue(1).ToString().Trim();
                                    }
                                    reader1.Dispose();

                                    if ((esttus == "P") && (rec_esp != ""))
                                    {
                                        cmnd1 = thisConnection.CreateCommand();
                                        cmnd1.CommandText = "update tb_mstr_recepcion_esparrago set prov_clave = '" + txtclaveprov.Text + "', rch_clave = '" + rch_clave + "', tbl_clave = '" + tabl + "', " +
                                                            "rmp_pesador = '" + CBRecBas.SelectedItem.ToString().Trim() + "', rmp_num_envase = '" + canti + "' " +
                                                            "where rmp_recibo = " + rec_esp + " and rmp_ticket = '" + txtticket.Text + "'";
                                        reader1 = cmnd1.ExecuteReader();
                                        reader1.Dispose();
                                    }
                                    else
                                    {
                                        cmnd1 = thisConnection.CreateCommand();
                                        cmnd1.CommandText = "insert into tb_mstr_recepcion_esparrago (rmp_fecha, rmp_ticket, prov_clave, rch_clave, tbl_clave, prod_clave, lin_clave, rmp_pesador, rmp_hora, rmp_num_envase, rmp_estatus)  " +
                                                            "values ('" + DTPFecha.Value.ToShortDateString() + "', '" + txtticket.Text + "', '" + txtclaveprov.Text + "', '" + rch_clave + "', " +
                                                            "'" + tabl + "', '" + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + "', '" + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + "', " +
                                                            "'" + CBRecBas.SelectedItem.ToString().Trim() + "', '" + txtpesohora.Text + "', '" + canti + "', 'P') " +
                                                            "select scope_identity()";
                                        rec_esp = Convert.ToString(cmnd1.ExecuteScalar()).Trim();
                                    }
                                    thisConnection.Close();

                                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "10.1", rec_esp, "REGISTRO DE NUEVO TICKET DE BASCULA " + txtticket.Text + " CON ESPARRAGO " + rec_esp, "SIPGAB");
                                }
                                #endregion

                                PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                                pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 307, 393);
                                pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                                //PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
                                //VistaPrevia.Document = pd;
                                //VistaPrevia.ShowDialog();
                                pd.Print();
                            }
                        }


                        Utilerias.Class1.registrar_movimiento(DateTime.Now, Utilerias.Class1.Nombre_equipo, Utilerias.Class1.Usuario, "M", "10.1", txtticket.Text, "MODIFICACION DE DATOS AL TICKET " + txtticket.Text + " DE BASCULA", "SIPGAB");

                        MessageBox.Show("El registro ha sido modificado con éxito", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        thisConnection.Close();
                        Utilerias.Class1.registro_errores(DateTime.Now, Utilerias.Class1.Usu_login, Environment.MachineName, "10.1", ex.ToString().Trim(), "MPSQL");
                        Utilerias.Class1.SendMail("jbravo@mrlucky.com.mx", "jbravo", "juanjose", ex.ToString().Trim());
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion

                limpiarTextBoxes(this);
                listBox1.Items.Clear();
                suma_prod = 0;
                productos = "";
                unities = "";
                peso_bruto = 0.0;
                peso_entrada = 0.0;
                peso_neto = 0.0;
                tara = 0.0;
                CBmasFletes.Checked = false;
                CBmasFletes.Enabled = false;
                cont = 0;
                txtpesohora.Value = DateTime.Now;
            }
            else
            {
                return;
            }
        }

        private void CBRecBas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                thisConnection.Open();
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select bas_clave from tb_cat_basculeros where bas_nombre = '" + CBRecBas.SelectedItem.ToString().Trim() + "'";
                reader1 = cmnd1.ExecuteReader();
                while (reader1.Read())
                {
                    bas_clave = Convert.ToInt32(reader1.GetValue(0).ToString());
                }
                reader1.Dispose();
                thisConnection.Close();
            }
            catch (SqlException ex)
            {
                thisConnection.Close();
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //método que limpia todo
        private void limpiarTextBoxes(Control parent)
        {
            //Limpiar de manera rapida
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    txt.Text = "";
                    txt.Enabled = false;
                }
                if (c.Controls.Count > 0)
                {
                    limpiarTextBoxes(c);
                }
            }

            foreach (Control c in parent.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.SelectedIndex = -1;
                    cb.Enabled = false;
                }
                if (c.Controls.Count > 0)
                {
                    limpiarTextBoxes(c);
                }
            }

            foreach (Control c in parent.Controls)
            {
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    rb.Checked = false;
                    rb.Enabled = false;
                }
                if (c.Controls.Count > 0)
                {
                    limpiarTextBoxes(c);
                }
            }


            DGV1.Rows.Clear();
            //CBlinea.Items.Clear();
            //CBProducto.Items.Clear();
            txtpesobruto.Text = "0.00";
            txtpesoent.Text = "0.00";
            txtpesoneto.Text = "0.00";
            txtpesosal.Text = "0.00";
            txttara.Text = "0.00";
            btnAlta.Enabled = true;
            btnConsulta.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancel.Enabled = false;
            lin = ""; prod = ""; numflete = ""; id_proveedor = ""; id_rancho = ""; origen = ""; id_responsable = ""; plataforma = ""; numero_economico = ""; chofer = ""; tipo_transporte = ""; id_linea = ""; tipo = ""; transportista = ""; rancho = ""; proveedor = ""; id_unidad = ""; id_variedad = ""; vehiculo = ""; productos = ""; unities = ""; clasificacion = ""; estatus = ""; clave_bas = ""; hay = ""; detalle = ""; envase = ""; rec_esp = ""; tabl = "";
            aux_id_rancho = ""; aux_observacion = ""; aux_num_economico = ""; aux_od = ""; prov_clave = ""; rch_clave = ""; tbl_clave = ""; tbl_nombre = "";
            aux_transporte = ""; aux_chofer = ""; aux_responsable = ""; aux_id_tipo_flete = ""; aux_id_responsable = ""; aux_origen = "";
            aux_valor_operador = ""; aux_valor_transportista = ""; consulta = "N";
            aux_id_proveedor = ""; aux_nombre_proveedor = ""; aux_opcion = ""; aux_presentacion = ""; texto = ""; tant = ""; aux_nom_variedad = ""; fecha_carga = "";
            lastId = 0; numero = 0; numero_renglones = 0; rencontrol = 0; coor_y_ima = 0; bas_clave = 0; opcion = 0;
            peso_entrada = 0; peso_bruto = 0; tara = 0; peso_neto = 0;
            id_ticket = 0; suma_prod = 0; cont = 0;
            nombre_unidad = "";
            canti = 0;
            esttus = "";
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            btnAlta.Enabled = false;
            btnConsulta.Enabled = false;
            btnCancel.Enabled = true;
            txtticket.ReadOnly = false;
            txtticket.Enabled = true;
            txtticket.Focus();
            btnmodi.Enabled = false;
            consulta = "S";
            txtclaveprov.Enabled = true;
            txtpesohora.Enabled = false;
        }

        //consulta el ticket de báscula
        private void txtticket_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {
                    //btnGuardar.Enabled = true;
                    //opcion = 1;
                    btnimpticket.Text = "IMPRIMIR TICKET FLETE";
                    btnimpticket.Enabled = false;
                    cargar_unidades(2);
                    for (int i = 0; i < variedad.Count; i++)
                    {
                        Column17.Items.Add(variedad[i]);
                    }
                    for (int i = 0; i < unidades.Count; i++)
                    {
                        Columna06.Items.Add(unidades[i]);
                    }

                    DGV1.ReadOnly = true;
                    DGV1.Rows.Clear();
                    CBRecBas.SelectedIndex = -1;
                    thisConnection.Open();
                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select fecha_entrada, cargado, folio, transportista, rancho, observacion, clasificacion, peso_entrada, tara, peso_bruto, " +
                                        "peso_neto, ISNULL(estatus, '') AS estatus, ISNULL(recibio_bascula, -1) AS recibio_bascula, error, error_comenta, prov_clave, hora_peso " +
                                        "from tb_mstr_recepcion_bascula  where id_ticket = " + Convert.ToInt32(txtticket.Text);
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        DTPFecha.Value = Convert.ToDateTime(reader1.GetValue(0).ToString());
                        if (reader1.GetValue(1).ToString().Trim().Equals("C"))
                            RBCar.Checked = true;
                        if (reader1.GetValue(1).ToString().Trim().Equals("V"))
                            RBvac.Checked = true;

                        txtregent.Text = reader1.GetValue(2).ToString().Trim();
                        txttrans.Text = reader1.GetValue(3).ToString().Trim();
                        txtrch.Text = reader1.GetValue(4).ToString().Trim();
                        txtobs.Text = reader1.GetValue(5).ToString().Trim();

                        if (reader1.GetValue(6).ToString().Trim().Equals("F"))
                            RBFlete.Checked = true;

                        if (reader1.GetValue(6).ToString().Trim().Equals("E"))
                            RBProvExt.Checked = true;

                        if (reader1.GetValue(6).ToString().Trim().Equals("C"))
                            RBCaVe.Checked = true;

                        if (reader1.GetValue(6).ToString().Trim().Equals("I"))
                            RBMovInt.Checked = true;

                        txtpesoent.Text = reader1.GetValue(7).ToString().Trim();
                        txtpesobruto.Text = reader1.GetValue(8).ToString().Trim();
                        txttara.Text = reader1.GetValue(9).ToString().Trim();
                        txtpesoneto.Text = reader1.GetValue(10).ToString().Trim();
                        estatus = reader1.GetValue(11).ToString().Trim();
                        bas_clave = Convert.ToInt32(reader1.GetValue(12).ToString().Trim());
                        prov_clave = reader1.GetValue(15).ToString().Trim();
                        txtpesohora.Text = reader1.GetValue(16).ToString().Trim();

                        if (reader1.GetValue(13).ToString().Trim().Equals("S"))
                        {
                            lbIndError.Visible = true;
                            lbindrev.Visible = true;
                            CBIndRev.SelectedIndex = 1;
                            txtIndError.Visible = true;
                            txtIndError.Text = reader1.GetValue(14).ToString().Trim();
                        }
                        else
                        {
                            lbIndError.Visible = false;
                            lbindrev.Visible = false;
                            CBIndRev.SelectedIndex = 0;
                            txtIndError.Visible = false;
                            txtIndError.Clear();
                        }

                    }
                    if (reader1.HasRows == false)
                    {
                        thisConnection.Close();
                        MessageBox.Show("No se encontro registro con el ticket de báscula: " + txtticket.Text, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarTextBoxes(this);
                        btnCancel.Enabled = true;
                        txtticket.Enabled = true;
                        txtticket.Focus();
                        return;
                    }
                    reader1.Dispose();
                    if (bas_clave != -1)
                    {
                        cmnd1 = thisConnection.CreateCommand();
                        cmnd1.CommandText = "select bas_nombre from tb_cat_basculeros where bas_clave = " + bas_clave;
                        reader1 = cmnd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            CBRecBas.SelectedItem = reader1.GetValue(0).ToString().Trim();
                        }
                        reader1.Dispose();
                    }


                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select prov_nombre from tb_cat_proveedor where prov_clave = '" + prov_clave + "'";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        CBProv.SelectedItem = reader1.GetValue(0).ToString();
                        txtclaveprov.Text = prov_clave;
                    }
                    reader1.Dispose();

                    ranch.Items.Clear();
                    foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "'"))
                    {
                        ranch.Items.Add(Convert.ToString(dr["rch_clave"].ToString()).Trim() + " - " + Convert.ToString(dr["rch_nombre"].ToString()).Trim());
                        //ranch.ValueMember = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                        //ranch.DisplayMember = Convert.ToString(dr["rch_nombre"].ToString()).Trim();
                    }
                    //cmnd1 = thisConnection.CreateCommand();
                    //cmnd1.CommandText = "select rch_nombre from tb_cat_ranchos where prov_clave = '" + prov_clave + "'";
                    //reader1 = cmnd1.ExecuteReader();
                    //while (reader1.Read())
                    //{
                    //    ranchos.Items.Add(reader1.GetValue(0).ToString().Trim());
                    //}
                    //reader1.Dispose();

                    //Connection.thisClose();

                    //for (int i = 0; i < ranchos.Items.Count; i++)
                    //{
                    //    ranch.Items.Add(ranchos.Items[i]);
                    //}
                    string rach = "";
                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select A.prod_clave, A.lin_clave, B.prod_nombre, A.cantidad, A.envase, A.peso_bruto, A.tara, A.peso_neto, A.id_tabla, " +
                                      "A.tabla, A.num_tarimas, A.caj_tarimas, A.variedad, A.observaciones, a.tipo_prod, A.estatus, A.rch_clave, A.flete from tb_det_recepcion_bascula A, " +
                                      "tb_cat_producto B where B.prod_clave = A.prod_clave and A.id_ticket = " + txtticket.Text;
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        //Columna06.Items.Add(reader1.GetValue(4).ToString().Trim());

                        //Column17.Items.Add(reader1.GetValue(12).ToString().Trim());                        
                        rch_clave = reader1.GetValue(16).ToString().Trim();
                        cmnd11 = thisConnection.CreateCommand();
                        cmnd11.CommandText = "select rch_nombre, rch_clave from tb_cat_ranchos where rch_clave = '" + reader1.GetValue(16).ToString().Trim() + "' and prov_clave = '" + prov_clave + "'";
                        reader11 = cmnd11.ExecuteReader();
                        while (reader11.Read())
                        {
                            for (int i = 0; i < ranch.Items.Count; i++)
                            {
                                if (ranch.Items[i].ToString().Trim() == reader11.GetValue(0).ToString().Trim())
                                    rach = reader11.GetValue(1).ToString().Trim() + " - " + ranch.Items[i].ToString().Trim();
                            }
                        }
                        reader11.Dispose();
                        DGV1.Rows.Add(reader1.GetValue(0).ToString().Trim(), reader1.GetValue(1).ToString().Trim(), reader1.GetValue(2).ToString().Trim(), reader1.GetValue(3).ToString(), reader1.GetValue(4).ToString().Trim(), reader1.GetValue(5).ToString().Trim(), reader1.GetValue(6).ToString().Trim(), reader1.GetValue(7).ToString().Trim(), reader1.GetValue(8).ToString().Trim(), reader1.GetValue(9).ToString().Trim(), reader1.GetValue(10).ToString().Trim(), reader1.GetValue(11).ToString().Trim(), reader1.GetValue(14).ToString().Trim(), reader1.GetValue(12).ToString().Trim(), "", reader1.GetValue(13).ToString().Trim(), reader1.GetValue(3).ToString().Trim(), reader1.GetValue(17).ToString().Trim(), prov_clave, rach, reader1.GetValue(15).ToString().Trim());
                    }
                    reader1.Dispose();
                    thisConnection.Close();


                    if (estatus == "R" || estatus == "C")
                    {
                        thisConnection.Close();
                        MessageBox.Show("Este ticket ya ha sido capturado en Materia Prima", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        btnmodi.Enabled = true;
                    }

                    for (int i = 0; i < DGV1.Rows.Count; i++)
                    {
                        if (Convert.ToString(DGV1.Rows[i].Cells[1].Value).Trim() == "08")
                        {
                            btnimpticket.Text = "IMPRIMIR TICKET ESP";
                            btnimpticket.Enabled = true;
                            //thisConnection.Open();
                            //cmnd1 = thisConnection.CreateCommand();
                            //cmnd1.CommandText = "select rmp_recibo from tb_mstr_recepcion_esparrago where rmp_ticket = '" + txtticket.Text + "'";
                            //rec_esp = Convert.ToString(cmnd1.ExecuteScalar());
                            //thisConnection.Close();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    thisConnection.Close();
                    MessageBox.Show(ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnmodi_Click(object sender, EventArgs e)
        {
            txtpesoent.Enabled = true;
            txttara.Enabled = true;
            txttrans.Enabled = true;
            txtrch.Enabled = true;
            GBVeh.Enabled = true;
            txtpesosal.Enabled = true;
            txtpesoneto.Enabled = true;
            GBTipo.Enabled = true;
            txtlin_clave.Enabled = true;
            CBlinea.Enabled = true;
            txtpesobruto.Enabled = true;
            txtobs.Enabled = true;
            txtprod_clave.Enabled = true;
            CBProducto.Enabled = true;
            CBRecBas.Enabled = true;
            txtticket.ReadOnly = true;
            DGV1.ReadOnly = false;
            CBProv.Enabled = true;
            txtpesohora.Enabled = true;
            //detalle = detalle + ", " + txtpesoent.Text + ", " + txttara.Text + ", "+ txttrans.Text + ", "+ txtrch.Text + ", " + vehiculo + ", " + txtpesosal.Text + ", " + txtpesoneto.Text + ", " +tipo + ", " +txtpesobruto.Text + ", " +txtobs.Text + ", "+ CBRecBas.SelectedItem.ToString() +",";
            //Utilerias.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Usu_login, "M", Utilerias.Formulario.Trim(), txtticket.Text, detalle);
            //detalle = "";

            //for (int i = 0; i < DGV1.Rows.Count; i++)
            //{
            //    detalle = detalle + Convert.ToString(DGV1.Rows[i].Cells[0].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[1].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[2].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[3].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[4].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[5].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[6].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[7].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[8].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[9].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[10].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[11].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[12].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[13].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[14].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[15].Value) + ", " + Convert.ToString(DGV1.Rows[i].Cells[16].Value);
            //    Utilerias.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Usu_login, "M", Utilerias.Formulario.Trim(), txtticket.Text, detalle);
            //}
            btnGuardar.Enabled = true;
            btnmodi.Enabled = false;
            opcion = 3;
            for (int i = 0; i < DGV1.Rows.Count; i++)
            {
                if (Convert.ToString(DGV1.Rows[i].Cells[20].Value) == "R")
                    DGV1.Rows[i].ReadOnly = true;

            }
        }

        private void CBProv_SelectionChangeCommitted(object sender, EventArgs e)
        {
            thisConnection.Open();
            string proveedor = CBProv.SelectedItem.ToString();
            proveedor = proveedor.Replace("'", "''");
            cmnd1 = thisConnection.CreateCommand();
            cmnd1.CommandText = "select prov_clave from tb_cat_proveedor where prov_nombre = '" + proveedor + "'";
            reader1 = cmnd1.ExecuteReader();
            while (reader1.Read())
            {
                prov_clave = reader1.GetValue(0).ToString().Trim();
                txtclaveprov.Text = prov_clave;
            }
            reader1.Dispose();
            thisConnection.Close();
            ranch.Items.Clear();
            foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "'"))
            {
                ranch.Items.Add(Convert.ToString(dr["rch_clave"].ToString()).Trim() + " - " + Convert.ToString(dr["rch_nombre"].ToString()).Trim());
            }
            //cmnd1 = thisConnection.CreateCommand();
            //cmnd1.CommandText = "select rch_clave, rch_nombre from tb_cat_ranchos where prov_clave = '" + prov_clave + "' order by rch_nombre";
            //reader1 = cmnd1.ExecuteReader();
            //while (reader1.Read())
            //{
            //    //ranchos.Items.Add(reader1.GetValue(1).ToString().Trim());
            //    ran.Items.Add(reader1.GetValue(1).ToString().Trim());
            //}
            //reader1.Dispose();            
            DGV1.Update();
        }

        ComboBox ran = new System.Windows.Forms.ComboBox();
        ComboBox tbl = new System.Windows.Forms.ComboBox();
        private void DGV1_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if (DGV1.CurrentCell.ColumnIndex == 19)
            {
                //if (consulta == "N")
                //{
                //ran.Items.Clear();

                ran = e.Control as ComboBox;
                if (ran != null)
                {
                    ran.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    ran.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
                //}
            }
            if (DGV1.CurrentCell.ColumnIndex == 9)
            {
                //if (consulta == "N")
                //{
                //tbl.Items.Clear();

                tbl = e.Control as ComboBox;
                if (tbl != null)
                {
                    tbl.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    tbl.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
                //}
                //else 
                //{
                //    tbl.Items.Clear();
                //    Column12.Items.Clear();
                //    thisConnection.Open();

                //    //cmnd1 = thisConnection.CreateCommand();
                //    //cmnd1.CommandText = "select rch_clave from tb_cat_ranchos where prov_clave = '" + prov_clave + "' and rch_nombre = '" + ran.SelectedItem.ToString().Trim() + "'";
                //    //reader1 = cmnd1.ExecuteReader();
                //    //while (reader1.Read())
                //    //{
                //    //    rch_clave = reader1.GetValue(0).ToString().Trim();
                //    //}
                //    //reader1.Dispose();

                //    cmnd1 = thisConnection.CreateCommand();
                //    cmnd1.CommandText = "select tbl_nombre FROM tb_cat_tablas where prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'";
                //    reader1 = cmnd1.ExecuteReader();
                //    while (reader1.Read())
                //    {
                //        tbl.Items.Add(reader1.GetValue(0).ToString().Trim());
                //    }
                //    reader1.Dispose();
                //    thisConnection.Close();

                //    for (int i = 0; i < tbl.Items.Count; i++)
                //    {
                //        Column12.Items.Add(tbl.Items[i]);
                //    }
                //}
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //thisConnection.Open();
            if (DGV1.CurrentCell.ColumnIndex == 19)
            {
                if (ran.SelectedIndex != -1)
                {
                    ran.SelectedItem = ran.SelectedIndex;
                    string rch = ran.SelectedItem.ToString().Trim();
                    //tbl.Items.Clear();
                    Column12.Items.Clear();

                    foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_nombre = '" + rch + "'"))
                    {
                        rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                        DGV1.Rows[DGV1.CurrentRow.Index].Cells[19].Value = rch;
                    }

                    foreach (DataRow dr in tablas.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'"))
                    {
                        //tbl.Items.Add(Convert.ToString(dr["tbl_nombre"].ToString()).Trim());
                        Column12.Items.Add(Convert.ToString(dr["tbl_nombre"].ToString()).Trim());
                    }
                }

                //for (int i = 0; i < tbl.Items.Count; i++)
                //{
                //    Column12.Items.Add(tbl.Items[i]);
                //}
            }
            if (DGV1.CurrentCell.ColumnIndex == 9)
            {
                if (tbl.SelectedIndex != -1)
                {
                    foreach (DataRow dr in tablas.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "' and tbl_nombre = '" + tbl.SelectedItem.ToString().Trim() + "'"))
                    {
                        DGV1.Rows[DGV1.CurrentRow.Index].Cells[8].Value = Convert.ToString(dr["tbl_clave"].ToString()).Trim();
                    }

                    //cmnd1 = thisConnection.CreateCommand();
                    //cmnd1.CommandText = "select tbl_clave from tb_cat_tablas where prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "' and tbl_nombre = '" + tbl.SelectedItem.ToString().Trim() + "'";
                    //reader1 = cmnd1.ExecuteReader();
                    //while (reader1.Read())
                    //{
                    //    DGV1.Rows[DGV1.CurrentRow.Index].Cells[8].Value = reader1.GetValue(0).ToString().Trim();

                    //}
                    //reader1.Dispose();
                    foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'"))
                    {
                        DGV1.Rows[DGV1.CurrentRow.Index].Cells[19].Value = Convert.ToString(dr["rch_nombre"].ToString()).Trim();
                    }


                    //cmnd1 = thisConnection.CreateCommand();
                    //cmnd1.CommandText = "select rch_nombre from tb_cat_ranchos where prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'";
                    //reader1 = cmnd1.ExecuteReader();
                    //while (reader1.Read())
                    //{
                    //    DGV1.Rows[DGV1.CurrentRow.Index].Cells[19].Value = reader1.GetValue(0).ToString().Trim();
                    //}
                    //reader1.Dispose();
                }
            }
            //DGV1.Update();
            //thisConnection.Close();
        }

        //private void tbl_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //DGV1.Rows[DGV1.CurrentRow.Index].Cells[19].Value = ran.SelectedItem.ToString();
        //    //ran.SelectedIndex = 1;
        ////    MessageBox.Show(DGV1.CurrentCell.Value.ToString());
        //////    thisConnection.Open();
        //////    cmnd1 = thisConnection.CreateCommand();
        //////    cmnd1.CommandText = "select tbl_nombre FROM tb_cat_tablas where prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'";
        //////    reader1 = cmnd1.ExecuteReader();
        //////    while (reader1.Read())
        //////    {
        //////        tbl.Items.Add(reader1.GetValue(0).ToString().Trim());
        //////    }
        //////    reader1.Dispose();
        //////    thisConnection.Close();
        //////    for (int i = 0; i < tbl.Items.Count; i++)
        //////    {
        //////        Column12.Items.Add(tbl.Items[i]);
        //////    }
        //}

        private void txtclaveprov_Click(object sender, EventArgs e)
        {
            txtclaveprov.SelectAll();
        }

        private void txtclaveprov_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (txtclaveprov.Text.Length > 0)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    thisConnection.Open();
                    cmnd1 = thisConnection.CreateCommand();
                    cmnd1.CommandText = "select prov_nombre from tb_cat_proveedor where prov_clave = '" + txtclaveprov.Text + "'";
                    reader1 = cmnd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        CBProv.SelectedItem = reader1.GetValue(0).ToString();
                    }
                    if (reader1.HasRows == false)
                    {
                        thisConnection.Close();
                        MessageBox.Show("No se encontro ningún proveedor con la clave: " + txtclaveprov.Text, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    reader1.Dispose();
                    thisConnection.Close();
                    ranch.Items.Clear();

                    foreach (DataRow dr in ranchos.Select("prov_clave ='" + txtclaveprov.Text + "'"))
                    {
                        //ranch.ValueMember = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                        //ranch.DisplayMember = Convert.ToString(dr["rch_nombre"].ToString()).Trim();
                        ranch.Items.Add(Convert.ToString(dr["rch_clave"].ToString()).Trim() + " - " + Convert.ToString(dr["rch_nombre"].ToString()).Trim());
                    }

                    prov_clave = txtclaveprov.Text;
                    //if (DGV1.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < DGV1.Rows.Count; i++)
                    //        DGV1.Rows.RemoveAt(i);
                    //}
                }
            }
        }


        private void DGV1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (DGV1.CurrentCell.ColumnIndex == 19)
            {
                if (ran.SelectedIndex != -1)
                {
                    ran.SelectedItem = ran.SelectedIndex;
                    //ranchos
                    string rc = ran.SelectedItem.ToString().Trim();
                    string[] rch = rc.Split('-');
                    //string rch  = ranchos.Rows[ran.SelectedIndex]["rch_clave"].ToString().Trim();
                    tbl.Items.Clear();
                    Column12.Items.Clear();
                    DGV1.Update();
                    //foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_nombre = '" + ran.SelectedItem.ToString().Trim() + "'"))
                    //foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                    //{
                    //    rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                    //}

                    foreach (DataRow dr in tablas.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'"))
                    {
                        //tbl.Items.Add(reader1.GetValue(0).ToString().Trim());
                        //Column12.Items.Add(reader1.GetValue(0).ToString().Trim());
                        tbl.Items.Add(Convert.ToString(dr["tbl_nombre"].ToString().Trim()));
                        Column12.Items.Add(Convert.ToString(dr["tbl_nombre"].ToString().Trim()));
                    }
                }
            }
        }



        private void DGV1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (DGV1.CurrentCell.ColumnIndex == 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DGV1.Rows.RemoveAt(DGV1.CurrentRow.Index);
                }
            }
        }

        private void DGV1_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (DGV1.CurrentCell.ColumnIndex == 8)
            {
                tabl = Convert.ToString(DGV1.CurrentRow.Cells[8].Value);
                bool cve = false;
                foreach (DataRow dr in tablas.Select("prov_clave = '" + txtclaveprov.Text + "' and rch_clave = '" + rch_clave + "' and tbl_clave = '" + tabl + "'"))
                {
                    DGV1.Rows[DGV1.CurrentRow.Index].Cells[9].Value = Convert.ToString(dr["tbl_nombre"].ToString()).Trim();
                    cve = true;
                }
                if (cve == false)
                {
                    MessageBox.Show("La clave de la tabla es incorrecta\r\nFavor de verificar la clave", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DGV1.CurrentRow.Cells[8].Value = "";
                    return;
                }
                cve = false;
            }
            if (DGV1.CurrentCell.ColumnIndex == 19)
            {
                if (ran != null)
                    ran.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);


                ran.SelectedItem = ran.SelectedIndex;
                //ranchos
                string rc = ran.SelectedItem.ToString().Trim();
                string[] rch = rc.Split('-');
                //string rch  = ranchos.Rows[ran.SelectedIndex]["rch_clave"].ToString().Trim();
                tbl.Items.Clear();
                Column12.Items.Clear();
                DGV1.Update();
                //foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_nombre = '" + ran.SelectedItem.ToString().Trim() + "'"))
                foreach (DataRow dr in ranchos.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch[0].ToString().Trim() + "'"))
                {
                    rch_clave = Convert.ToString(dr["rch_clave"].ToString()).Trim();
                }

                foreach (DataRow dr in tablas.Select("prov_clave = '" + prov_clave + "' and rch_clave = '" + rch_clave + "'"))
                {
                    //tbl.Items.Add(reader1.GetValue(0).ToString().Trim());
                    //Column12.Items.Add(reader1.GetValue(0).ToString().Trim());
                    tbl.Items.Add(Convert.ToString(dr["tbl_nombre"].ToString().Trim()));
                    Column12.Items.Add(Convert.ToString(dr["tbl_nombre"].ToString().Trim()));
                }
            }
            if (DGV1.Columns[e.ColumnIndex].Name == "rpt_rec")
            {
                thisConnection.Open();
                cmnd1 = thisConnection.CreateCommand();
                cmnd1.CommandText = "select * from tb_det_recepcion_pt where rpt_recibo = '" + DGV1.Rows[e.RowIndex].Cells["rpt_rec"].Value + "' and " +
                                    "prod_clave= '" + DGV1.Rows[e.RowIndex].Cells["Columna03"].Value + "'";
                reader1 = cmnd1.ExecuteReader();
                if (reader1.HasRows == false)
                {
                    thisConnection.Close();
                    MessageBox.Show("No existe número de recibo, favor de verificarlo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DGV1.Rows[e.RowIndex].Cells["rpt_rec"].Value = "";
                    return;
                }
                thisConnection.Close();
            }
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblPesoBas.Text = System.DateTime.Now.ToString("HH:mm ss");
        }

        private void BtnTicket_Click(object sender, EventArgs e)
        {
            FrmTicketBascula FrmTick = new FrmTicketBascula();
            FrmTick.ShowDialog(this);
        }

        private void CmbTktPen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbTktPen.SelectedIndex > -1)
            {
                string Tick = CmbTktPen.Text.Substring(0, CmbTktPen.Text.IndexOf("->") - 1);
                btnConsulta_Click(sender, e);
                txtticket.Text = Tick;
                txtticket_KeyPress(this, new KeyPressEventArgs((char)(Keys.Enter)));
                btnmodi_Click(sender, e);
            }
        }
    }
}