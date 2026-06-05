using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Printing;

namespace Recepcion_Bascula
{
    public partial class FrmTicketBascula : Form
    {
        SqlConnection thisConeccion = new SqlConnection(Utilerias.Class1.ConnectionString);
        DataTable Ticket = new DataTable();
        DataTable Tickets = new DataTable();

        public FrmTicketBascula()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);      
            //DtFec.Format = DateTimePickerFormat.Custom;
            //DtFec.CustomFormat = "dd/MM/yyyy HH:mm:ss";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LblPesoBas.Text = System.DateTime.Now.ToString("HH:mm:ss");
            LblHora.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }

        private void FrmTicketBascula_Load(object sender, EventArgs e)
        {
            this.Size = new Size(852, 608);
            CmbNombre.Items.Add("ANGEL");
            CmbNombre.Items.Add("FERNANDO");
            CmbNombre.Items.Add("OCTAVIO");
            CmbNombre.Items.Add("DIEGO");
            CmbNombre.Items.Add("ISAIAS");
            CmbNombre.Items.Add("ULISES");
            BuscaPendiente("B","0");
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            btnAlta.Enabled = false;
            thisConeccion.Open();
            string Cadena = "SELECT isnull(MAX(ID_TICKET),0) AS TICKET FROM TB_MSTR_TICKET_BASCULA";
            SqlCommand cmd = new SqlCommand(Cadena, thisConeccion);
            int IdTic = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            txtticket.Text = IdTic.ToString();
            LblConse.Text = "1";
            thisConeccion.Close();
            btnGuardar.Enabled = true;
            btnCancel.Enabled = true;
            BtnImp.Enabled = false;
            BtnTic.Enabled = false;
            btnConsulta.Enabled = false;
            habilita(true, "A");
        }

        private void Limpia()
        {
            txtticket.Text = "0";
            CmbTipo.SelectedIndex = -1;
            CmbNombre.SelectedIndex = -1;
            txttrans.Text = "";
            TxtPla.Text = "";
            DGDatos.Rows.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Limpia();
            btnGuardar.Enabled = false;
            btnAlta.Enabled = true;
            btnCancel.Enabled = false;
            PbxSel.Visible = false;
            PbxSel.Image = null;
            LbxTick.Enabled = true;
            this.Size = new Size(852, 608);
            BtnImp.Enabled = false;
            BtnTic.Enabled = false;
            btnConsulta.Enabled = true;
            txtticket.Enabled = false;
            TxtChofer.Enabled = false;
            TxtRan.Enabled = false;
            TxtProd.Enabled = false;
            TxtPla.Enabled = false;
            txttrans.Enabled = false;
            CmbTipo.Enabled = false;
            CmbNombre.Enabled = false;
            LblConse.Text = "0";
            ChkCampo.Checked = false;
            TxtChofer.Text = "";
            TxtRan.Text = "";
            TxtProd.Text = "";
            txttrans.Text = "";
            TxtPla.Text = "";
            ChkCampo.Checked = false;
            //if (LbxTick.Items.Count == 0)
            BuscaPendiente("B", "0");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mtip = (CmbTipo.Text.Trim() == "NACIONAL") ? "N" : "E";
            int Conse = Convert.ToInt32(LblConse.Text);
            if (Conse > 1 && Mtip == "E" && PbxSel.Image == null)
            {
                MessageBox.Show("No se ha Seleccionado la Imagen del Eje","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }
            if (Conse > 1)
                if ((Mtip == "N") || (Mtip == "E" && LblConse.Text == "5"))
                {
                    if ((txttrans.Text.Trim().Length == 0 || txttrans.Text.Trim().Length == 0) || (TxtChofer.Text.Trim().Length == 0 || TxtRan.Text.Trim().Length == 0 || TxtProd.Text.Trim().Length == 0  && ChkCampo.Checked))
                    {
                        MessageBox.Show("Falta Llenar Algunos Campos de Información Verifique!!! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            
            thisConeccion.Open();
            string mCam = (ChkCampo.Checked) ? "S" : "N";
            string Cadena = "Insert Into tb_mstr_Ticket_Bascula(id_ticket, tkt_Fecha, tkt_Hora, tkt_Conse, tkt_Peso, tkt_Tipo, tkt_Nombre, tkt_Placa, tkt_Transporte, tkt_totConse, tkt_Campo) " +
                "Values ('" + txtticket.Text + "','" + DtFec.Value.ToShortDateString() + "','" + LblHora.Text + "','"+ LblConse.Text +"','" + Convert.ToInt32(LblHora.Text.Substring(0, 2)) * 100 + LblHora.Text.Substring(3, 2) + "','" + Mtip + "','" + CmbNombre.Text.Trim() +
                "','" + TxtPla.Text.Trim() + "','" + txttrans.Text.Trim() + "','1','"+mCam +"')";
            SqlCommand cmd = new SqlCommand(Cadena, thisConeccion);
            cmd.ExecuteNonQuery();
            if (Conse > 1)
            {
                if ((Mtip == "N") || (Mtip == "E" && LblConse.Text == "5"))
                {
                    Cadena = "Update tb_mstr_Ticket_Bascula set tkt_totConse = '2' Where id_ticket = '" + txtticket.Text + "'";
                    cmd = new SqlCommand(Cadena, thisConeccion);
                    cmd.ExecuteNonQuery();
                    Cadena = "Update tb_mstr_Ticket_Bascula set tkt_transporte = '"+  txttrans.Text.Trim() +"', tkt_placa = '" + TxtPla.Text.Trim() + "', tkt_Chofer = '"+ TxtChofer.Text.Trim() + "'," +
                        "tkt_Rancho = '"+ TxtRan.Text.Trim() +"', tkt_Producto = '"+ TxtProd.Text.Trim() +"' Where id_ticket = '" + txtticket.Text + "' and tkt_conse = '"+ LblConse.Text +"'";
                    cmd = new SqlCommand(Cadena, thisConeccion);
                    cmd.ExecuteNonQuery();
                }
            }
            thisConeccion.Close();
            if (Mtip == "E" && Conse <= 5)
            {
                MessageBox.Show("Eje Grabado Correctamente!!!");
                string mPe = Convert.ToInt32(LblHora.Text.Substring(0, 2)) * 100 + LblHora.Text.Substring(3, 2);
                DGDatos.Rows.Add(DtFec.Value.ToShortDateString(), LblHora.Text, "0", mPe , "0", mPe);
                string Cad = DtFec.Value.ToShortDateString() + " " + LblHora.Text + " Gross " + mPe;  
                LblConse.Text = (Conse + 1).ToString();
                switch (Conse)
                {
                    case 2:
                        LblEje1.Text = Cad;
                        LblPenE1.Visible = false;
                        PbxEje1.Enabled = false;
                        break;
                    case 3:
                        LblEje12.Text = Cad;
                        LblPenE12.Visible = false;
                        PbxEje12.Enabled = false;
                        break;
                    case 4:
                        LblEje3.Text = Cad;
                        LblPenE3.Visible = false;
                        PbxEje3.Enabled = false;
                        break;
                    case 5:
                        LblEje123.Text = Cad;
                        LblPenE123.Visible = false;
                        PbxEje123.Enabled = false;
                        break;
                }
                PbxSel.Image = null;
            }
            if (Conse == 1 || (Conse == 2 && Mtip == "N") || Conse == 5)
            {
                MessageBox.Show("Ticket Guardado Correctamente");
                if ((Conse == 2 && Mtip == "N") || Conse == 5)
                {
                    if (Conse == 2 && Mtip == "N")
                        DGDatos.Rows.Add(DtFec.Value.ToShortDateString(), LblHora.Text, "0", Convert.ToInt32(LblHora.Text.Substring(0, 2)) * 100 + LblHora.Text.Substring(3, 2), "0", Convert.ToInt32(LblHora.Text.Substring(0, 2)) * 100 + LblHora.Text.Substring(3, 2));
                    BtnTic_Click(sender, e);
                }
                if (Conse == 5) // ticket de exportacion
                    BtnImp_Click(sender, e);
                Limpia();
                btnGuardar.Enabled = false;
                btnCancel.Enabled = false;
                btnAlta.Enabled = true;
                BuscaPendiente("B","0");
                LbxTick.Enabled = true;
                BtnImp.Enabled = false;
                BtnTic.Enabled = false;
                if ((Conse == 2 && Mtip == "N") || Conse == 5)
                    btnCancel_Click(sender, e);
            }
            
            
        }

        private void BuscaPendiente(string Op, String mTik)
        {
            LbxTick.Items.Clear();
            thisConeccion.Open();
            string Cadena = "SELECT * FROM TB_MSTR_TICKET_BASCULA WHERE TKT_TOTCONSE = 1 ORDER BY ID_TICKET, TKT_CONSE ";
            if (Op == "C") // Consultar x No de Ticket
                Cadena = "SELECT * FROM TB_MSTR_TICKET_BASCULA WHERE ID_TICKET = '"+ mTik +"' ORDER BY ID_TICKET, TKT_CONSE ";
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConeccion);
            DataSet ds = new DataSet();
            da.Fill(ds, "Info");
            Ticket = ds.Tables["Info"];
            if (Op != "C")
            {
                foreach (DataRow row in Ticket.Rows)
                {
                    if (Convert.ToInt32(row["tkt_conse"]) != 1)
                        continue;
                    LbxTick.Items.Add(row["id_ticket"].ToString());
                    LbxTick.Items.Add(row["tkt_placa"].ToString().Trim() + " -> " + row["tkt_transporte"].ToString().Trim());
                    LbxTick.Items.Add("");
                }
            }
            thisConeccion.Close();
        }
        
        private void LbxTick_DoubleClick(object sender, EventArgs e)
        {
            Int32 Numero = 0;
            if (!Int32.TryParse(LbxTick.SelectedItem.ToString(), out Numero))
            {
                MessageBox.Show("Seleccionar el Numero de Ticket", "Info", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            habilita(true,"C");
            LLenaInfo(Numero);
            btnCancel.Enabled = true;
            btnGuardar.Enabled = true;
            btnAlta.Enabled = false;
            btnConsulta.Enabled = false;
        }

        private void LLenaInfo(int Ticke)
        {
            DGDatos.Rows.Clear();
            string mtip = "";
            LblEje1.Text = "PENDIENTE";
            LblEje12.Text = "PENDIENTE";
            LblEje123.Text = "PENDIENTE";
            LblEje3.Text = "PENDIENTE";
            LblPenE1.Visible = true;
            LblPenE12.Visible = true;
            LblPenE3.Visible = true;
            LblPenE123.Visible = true;
            ChkCampo.Checked = false;
            //PbxEje1.Enabled = false;
            //PbxEje12.Enabled = false;
            //PbxEje3.Enabled = false;
            //PbxEje123.Enabled = false;
            foreach (DataRow row in Ticket.Select("Id_Ticket = '" + Ticke.ToString() + "'"))
            {
                decimal Pini = 0, PFin = 0;
                int mCons = Convert.ToInt32(row["tkt_conse"]); 
                if (mCons == 1)
                {
                    txtticket.Text = row["id_ticket"].ToString();
                    CmbTipo.Text = (row["tkt_Tipo"].ToString().Trim() == "N") ? "Nacional" : "Exportacion";
                    mtip = row["tkt_Tipo"].ToString();
                    CmbNombre.Text = row["tkt_Nombre"].ToString();
                    txttrans.Text = row["tkt_transporte"].ToString().Trim();
                    TxtPla.Text = row["tkt_placa"].ToString().Trim();
                    if (row["tkt_campo"].ToString().Trim() == "S")
                        ChkCampo.Checked = true;
                    DGDatos.Rows.Add(row["TKT_FECHA"].ToString(), row["TKT_HORA"].ToString(), row["tkt_peso"].ToString(), "0", "0", "0");
                    Pini = Convert.ToDecimal(row["tkt_peso"].ToString());
                    if (LblConse.Text != "C")
                        LblConse.Text = (Convert.ToInt32(row["tkt_conse"].ToString()) + 1).ToString();
                    continue;
                }
                if (mCons == 2 || mCons == 5)
                {
                    CmbNombre.Text = row["tkt_Nombre"].ToString();
                    txttrans.Text = row["tkt_transporte"].ToString().Trim();
                    TxtPla.Text = row["tkt_placa"].ToString().Trim();
                    if (row["tkt_campo"].ToString().Trim() == "S")
                        ChkCampo.Checked = true;
                    TxtChofer.Text = row["tkt_Chofer"].ToString().Trim();
                    TxtRan.Text = row["tkt_Rancho"].ToString().Trim();
                    TxtProd.Text =  row["tkt_Producto"].ToString().Trim();
                }
                if (LblConse.Text != "C")
                    LblConse.Text = (Convert.ToInt32(row["tkt_conse"].ToString()) + 1).ToString();
                
                if (mtip == "N")
                    DGDatos.Rows.Add(row["TKT_FECHA"].ToString(), row["TKT_HORA"].ToString(), "0", row["tkt_peso"].ToString(), Pini, Convert.ToDecimal(row["tkt_peso"].ToString()) - Pini);
                else
                {
                    DGDatos.Rows.Add(row["TKT_FECHA"].ToString(), row["TKT_HORA"].ToString(), "0", row["tkt_peso"].ToString(), "0", Convert.ToDecimal(row["tkt_peso"].ToString()));
                    
                    switch (mCons)
                    {
                        case 2:
                            LblEje1.Text = Convert.ToDateTime(row["TKT_FECHA"]).ToShortDateString() + " " + row["TKT_HORA"].ToString() + " Gross " + Convert.ToInt32(row["tkt_peso"]).ToString("#,##0");
                            LblPenE1.Visible = false;
                            PbxEje1.Enabled = false;
                            if (LblConse.Text != "C")
                            {
                                PbxEje12.Enabled = true;
                                PbxEje3.Enabled = true;
                                PbxEje123.Enabled = true;
                            }
                            break;
                        case 3:
                            LblEje12.Text = Convert.ToDateTime(row["TKT_FECHA"]).ToShortDateString() + " " + row["TKT_HORA"].ToString() + " Gross " + Convert.ToInt32(row["tkt_peso"]).ToString("#,##0");
                            LblPenE12.Visible = false;
                            PbxEje12.Enabled = false;
                            if (LblConse.Text != "C")
                            {
                                PbxEje3.Enabled = true;
                                PbxEje123.Enabled = true;
                            }
                            break;
                        case 4:
                            LblEje3.Text = Convert.ToDateTime(row["TKT_FECHA"]).ToShortDateString() + " " + row["TKT_HORA"].ToString() + " Gross " + Convert.ToInt32(row["tkt_peso"]).ToString("#,##0");
                            LblPenE3.Visible = false;
                            PbxEje3.Enabled = false;
                            if (LblConse.Text != "C")
                                PbxEje123.Enabled = true;
                            break;
                        case 5:
                            LblEje123.Text = Convert.ToDateTime(row["TKT_FECHA"]).ToShortDateString() + " " + row["TKT_HORA"].ToString() + " Gross " + Convert.ToInt32(row["tkt_peso"]).ToString("#,##0");
                            LblPenE123.Visible = false;
                            PbxEje123.Enabled = false;
                            break;
                    }
                }
            }
            if (mtip == "E")
            {
                this.Size = new Size(1092, 608);
                PbxSel.Visible = true;
            }
            CreaQr(txtticket.Text);
            LbxTick.Enabled = false;
            BtnImp.Enabled = true;
            BtnTic.Enabled = true;
            DGDatos.Focus();
        }

        private void habilita(Boolean op, String Opci)
        {
            CmbTipo.Enabled = op;
            CmbNombre.Enabled = op;
            ChkCampo.Enabled = op;
            TxtPla.Enabled = op;
            txttrans.Enabled = op;
            DtFec.Enabled = op;
            TxtChofer.Enabled = op;
            TxtRan.Enabled = op;
            TxtProd.Enabled = op;
            if (Opci == "C")
            {
                CmbTipo.Enabled = !op;
                CmbNombre.Enabled = !op;
                ChkCampo.Enabled = !op;
            }
            
        }

        private void PbxEje1_Click(object sender, EventArgs e)
        {
            if (valida(2))
            {
                MessageBox.Show("Error Falta Capturar el Eje Anterior!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            PbxSel.Image = PbxEje1.Image;
        }

        private void PbxEje12_Click(object sender, EventArgs e)
        {
            if (valida(3))
            {
                MessageBox.Show("Error Falta Capturar el Eje Anterior!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            PbxSel.Image = PbxEje12.Image;
        }

        private void PbxEje3_Click(object sender, EventArgs e)
        {
            if (valida(4))
            {
                MessageBox.Show("Error Falta Capturar el Eje Anterior!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            PbxSel.Image = PbxEje3.Image;
        }

        private void PbxEje123_Click(object sender, EventArgs e)
        {
            if (valida(5))
            {
                MessageBox.Show("Error Falta Capturar el Eje Anterior!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            PbxSel.Image = PbxEje123.Image;
        }

        private Boolean valida(int conse)
        {
            Boolean op = false;
            if (Convert.ToInt32(LblConse.Text) < conse)
                op = true;
            return op;
        }

        private void PdTicket_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            SolidBrush color = new SolidBrush(Color.Black);
            Font fuente = new Font("Courier", 10);
            Font fuenteTic = new Font("Courier", 12, FontStyle.Bold);
            Font fuente2 = new Font("Courier", 7, FontStyle.Bold);
            Font fuente3 = new Font("Arial", 6, FontStyle.Bold);
            Font fuente4 = new Font("Arial", 4);
            Font fuente5 = new Font("Courier", 7, FontStyle.Italic);
            Font cod_bar = new Font("PF Barcode 39", 30);//código de barras  
            e.Graphics.DrawImage(PbxLogoMr.Image, 5, 15, 50, 50);
            e.Graphics.DrawImage(PbxLogoGab.Image, 220, 15, 75, 25);
            Point rec = new System.Drawing.Point(80, 15);
            e.Graphics.DrawString("COMERCIALIZADORA GAB", fuente3, color, rec);
            rec = new System.Drawing.Point(110, 24);
            e.Graphics.DrawString("S.A. DE C.V.", fuente3, color, rec);
            rec = new System.Drawing.Point(100, 35);
            e.Graphics.DrawString("R.F.C. CGA-960614-2C5", fuente4, color, rec);
            rec = new System.Drawing.Point(85, 41);
            e.Graphics.DrawString("CARR. PANAMERICANA KM. 291-1", fuente4, color, rec);
            rec = new System.Drawing.Point(80, 48);
            e.Graphics.DrawString("CORTAZAR, GTO. MEXICO. C.P. 38495", fuente4, color, rec);
            rec = new System.Drawing.Point(85, 55);
            e.Graphics.DrawString("TELS 462-626-2663 / 462-626-2939", fuente4, color, rec);
            int RegFin = DGDatos.Rows.Count - 1;
            Decimal Tara = 0, Peso = 0;
            int pos = 0;
            string HI = "";
            foreach (DataGridViewRow  row in DGDatos.Rows)
            {
                if (pos == 0)
                {
                    Tara = Convert.ToDecimal(DGDatos.Rows[pos].Cells["WEIGHT"].Value);
                    rec = new System.Drawing.Point(70, 70);
                    e.Graphics.DrawString("No. " + txtticket.Text, fuenteTic, color, rec);
                    rec = new Point(70, 90);
                    e.Graphics.DrawString("Date: " + Convert.ToDateTime(DGDatos.Rows[pos].Cells["fecha"].Value).ToShortDateString(), fuente, color, rec);
                    rec = new Point(70, 105);
                    e.Graphics.DrawString("Inbound Time: " + DGDatos.Rows[pos].Cells["hora"].Value.ToString(), fuente, color, rec);
                    HI = DGDatos.CurrentRow.Cells["hora"].Value.ToString();
                    rec = new Point(70, 120);
                    e.Graphics.DrawString("Transaction 1: ", fuente, color, rec);
                    rec = new Point(70, 135);
                    e.Graphics.DrawString("Weight: " + Tara.ToString("#,###") + " Kg", fuente, color, rec);
                }
                if (pos == RegFin)
                {
                    Peso = Convert.ToDecimal(DGDatos.Rows[pos].Cells["GROSS"].Value);
                    rec = new Point(70, 160);
                    e.Graphics.DrawString("Date: " + Convert.ToDateTime(DGDatos.Rows[pos].Cells["FECHA"].Value).ToShortDateString(), fuente, color, rec);
                    rec = new Point(70, 175);
                    e.Graphics.DrawString("Inbound Time: " + HI, fuente, color, rec);
                    rec = new Point(70, 190);
                    e.Graphics.DrawString("Outbound Time: " + DGDatos.Rows[pos].Cells["HORA"].Value.ToString(), fuente, color, rec);
                    rec = new Point(70, 215);
                    e.Graphics.DrawString("Gross: " + Peso.ToString("#,###") + " Kg", fuente, color, rec);
                    rec = new Point(70, 230);
                    e.Graphics.DrawString("Tare: " + Tara.ToString("#,###") + " Kg", fuente, color, rec);
                    rec = new Point(70, 245);
                    e.Graphics.DrawString("Net: " + (Peso - Tara).ToString("#,###") + " Kg", fuente, color, rec);
                }
                pos++;
            }
            rec = new Point(10, 285);
            e.Graphics.DrawString("NOMBRE_______________________________________", fuente2, color, rec);
            rec = new Point(70, 285);
            e.Graphics.DrawString(TxtChofer.Text.Trim() , fuente5, color, rec);
            rec = new Point(10, 310);
            e.Graphics.DrawString("RANCHO____________________________________", fuente2, color, rec);
            rec = new Point(70, 310);
            e.Graphics.DrawString(TxtRan.Text.Trim(), fuente5, color, rec);
            rec = new Point(10, 335);
            e.Graphics.DrawString("PRODUCTO_____________________________________" , fuente2, color, rec);
            rec = new Point(90, 335);
            e.Graphics.DrawString(TxtProd.Text.Trim(), fuente5, color, rec);
            rec = new Point(250, 355);
            e.Graphics.DrawString("F-500-18", fuente4, color, rec);
            rec = new Point(250, 361);
            e.Graphics.DrawString("REV. 01", fuente4, color, rec);

        }

        private void BtnImp_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            // ticket bascula
            //pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 310, 400);
            //pd.PrintPage += new PrintPageEventHandler(this.PdTicket_PrintPage);
            //ticket Ejes
            pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 310, 500);
            pd.PrintPage += new PrintPageEventHandler(PdTicketExp_PrintPage);
            PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
            VistaPrevia.Document = pd;
            VistaPrevia.ShowDialog();
            PrintDialog Imp = new PrintDialog();
            Imp.Document = pd;
            if (Imp.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void PdTicketExp_PrintPage(object sender, PrintPageEventArgs e)
        {
            SolidBrush color = new SolidBrush(Color.Black);
            Font fuente = new Font("Courier", 10);
            Font fuenteTic = new Font("Courier", 12, FontStyle.Bold);
            Font fuente2 = new Font("Courier", 7, FontStyle.Bold);
            Font fuente3 = new Font("Arial", 8);
            Font cod_bar = new Font("PF Barcode 39", 30);//código de barras  
            Pen box = new Pen(color);
            e.Graphics.DrawRectangle(box, 5, 15, 300, 30);
            Point rec = new System.Drawing.Point(10, 15);
            e.Graphics.DrawString("COMERCIALIZADORA GAB          CONTROL", fuente, color, rec);
            rec = new System.Drawing.Point(100, 30);
            e.Graphics.DrawString("PESO POR EJES", fuente, color, rec);
            e.Graphics.DrawRectangle(box, 5, 50, 300, 20);
            rec = new System.Drawing.Point(10, 55);
            e.Graphics.DrawString("ELABORO: "+CmbNombre.Text.Trim(), fuente3, color, rec);
            rec = new System.Drawing.Point(190, 51);
            e.Graphics.DrawString("No. " + txtticket.Text, fuenteTic, color, rec);
            e.Graphics.DrawRectangle(box, 5, 90, 300, 70);
            e.Graphics.DrawImage(PbxEje1.Image, 200, 95, 100, 60);
            Int32 pos = 95;
            //foreach (DataRow row in Ticket.Select("Id_Ticket = '" + txtticket.Text + "'"))
            int nreg = 0;
            foreach (DataGridViewRow row in DGDatos.Rows)
            {
                if (nreg == 0)
                {
                    nreg++;
                    continue;
                }
                rec = new System.Drawing.Point(50, pos);
                e.Graphics.DrawString(DGDatos.Rows[nreg].Cells["HORA"].Value.ToString() + " " + Convert.ToDateTime(DGDatos.Rows[nreg].Cells["FECHA"].Value).ToShortDateString(), fuente2, color, rec);
                rec = new System.Drawing.Point(50, pos + 20); // 105
                e.Graphics.DrawString("GROSS: " + Convert.ToDecimal(DGDatos.Rows[nreg].Cells["GROSS"].Value).ToString("#,##0"), fuente3, color, rec);
                if (nreg < 4)
                {
                    rec = new System.Drawing.Point(10, pos + 20); // 105
                    e.Graphics.DrawString((nreg).ToString(), fuenteTic, color, rec);
                    rec = new System.Drawing.Point(22, pos + 20); // 105
                    e.Graphics.DrawString("o", fuente3, color, rec);
                }
                rec = new System.Drawing.Point(50, pos + 35); //120
                e.Graphics.DrawString("TARE:        0 KG", fuente3, color, rec);
                rec = new System.Drawing.Point(50, pos + 50); //135
                e.Graphics.DrawString("NET:  " + Convert.ToDecimal(DGDatos.Rows[nreg].Cells["GROSS"].Value).ToString("#,##0"), fuente3, color, rec);
                pos += 70;
                nreg++;
            }
            rec = new System.Drawing.Point(10, 325); // 
            e.Graphics.DrawString("Tot", fuenteTic, color, rec);
            e.Graphics.DrawRectangle(box, 5, 160, 300, 70);
            e.Graphics.DrawImage(PbxEje12.Image, 200, 165, 100, 60);
            e.Graphics.DrawRectangle(box, 5, 230, 300, 70);
            e.Graphics.DrawImage(PbxEje3.Image, 200, 235, 100, 60);
            e.Graphics.DrawRectangle(box, 5, 300, 300, 70);
            e.Graphics.DrawImage(PbxEje123.Image, 200, 305, 100, 60);
            e.Graphics.DrawRectangle(box, 5, 390, 300, 100);
            e.Graphics.DrawRectangle(box, 5, 390, 300, 20);
            rec = new System.Drawing.Point(10, 395);
            e.Graphics.DrawString("TRANSPORTE: ", fuente2, color, rec);
            rec = new System.Drawing.Point(100, 395);
            e.Graphics.DrawString(txttrans.Text.Trim(), fuente3, color, rec);
            e.Graphics.DrawRectangle(box, 5, 390, 300, 40);
            rec = new System.Drawing.Point(10, 415);
            e.Graphics.DrawString("PLACA: ", fuente2, color, rec);
            rec = new System.Drawing.Point(100, 415);
            e.Graphics.DrawString(TxtPla.Text, fuente3, color, rec);
            e.Graphics.DrawImage(PbxLogoMr.Image, 10, 440, 35, 35);
            e.Graphics.DrawImage(PbxLogoGab.Image, 220, 445, 70, 20);
            e.Graphics.DrawImage(PbxQR.Image, 110, 435, 55, 55);  
            //rec = new System.Drawing.Point(100, 440);
            //e.Graphics.DrawString("*"+txtticket.Text+"*", cod_bar, color, rec);
        }

        private void CreaQr(string Folio)
        {
            BarcodeLib.Barcode.QRCode qrbarcode = new BarcodeLib.Barcode.QRCode();

            // Select QR Code data encoding type: numeric, alphanumeric, byte, and Kanji to select from.
            qrbarcode.Encoding = BarcodeLib.Barcode.QRCodeEncoding.Auto;
            qrbarcode.Data = Folio;

            // Adjusting QR Code barcode module size and quiet zones on four sides.
            qrbarcode.ModuleSize = 1;
            qrbarcode.LeftMargin = 1; //12
            qrbarcode.RightMargin = 1; //12 
            qrbarcode.TopMargin = 1; //12
            qrbarcode.BottomMargin = 1; //12

            // Select QR Code Version (Symbol Size), available from V1 to V40, i.e. 21 x 21 to 177 x 177 modules.
            //qrbarcode.Version = BarcodeLib.Barcode.QRCodeVersion.V3; // V1

            // Set QR-Code bar code Reed Solomon Error Correction Level: L(7%), M (15%), Q(25%), H(30%)
            //qrbarcode.ECL = BarcodeLib.Barcode.QRCodeErrorCorrectionLevel.H; //L
            //qrbarcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;

            // More barcode settings here, like ECI, FNC1, Structure Append, etc.

            // save barcode image into your system
            //if (File.Exists(@"c:/reportes/qrcode.png"))
            //    File.Delete(@"c:/reportes/qrcode.png");
            //qrbarcode.drawBarcode(@"c:/reportes/qrcode.png");

            // Generate QR Code barcode & output to byte array
            byte[] barcodeInBytes = qrbarcode.drawBarcodeAsBytes();

            MemoryStream ms = new MemoryStream(barcodeInBytes, 0, barcodeInBytes.Length);
            ms.Write(barcodeInBytes, 0, barcodeInBytes.Length);
            Image newImage = Image.FromStream(ms, true);//Exception occurs here
            PbxQR.Image = newImage;


            // Generate QR Code barcode to Graphics object
            //Graphics graphics =  ... ;
            //barcode.drawBarcode(graphics);

            // Generate QR Code barcode and output to HttpResponse object
            //HttpResponse response = ...;
            //qrbarcode.drawBarcode(response);

            // Generate QR Code barcode and output to Stream object
            //Stream stream = "@c:\reporte\qr";
            //qrbarcode.drawBarcode(stream);
        }

        private void BtnTic_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            // ticket bascula
            pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 310, 400);
            pd.PrintPage += new PrintPageEventHandler(this.PdTicket_PrintPage);
            //ticket Ejes
            //pd.DefaultPageSettings.PaperSize = new PaperSize("etiqueta", 310, 500);
            //pd.PrintPage += new PrintPageEventHandler(PdTicketExp_PrintPage);
            PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
            VistaPrevia.Document = pd;
            VistaPrevia.ShowDialog();
            PrintDialog Imp = new PrintDialog();
            Imp.Document = pd;
            if (Imp.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            txtticket.Enabled = true;
            btnCancel.Enabled = true;
            BtnImp.Enabled = true;
            BtnTic.Enabled = true;
            //txtticket.SelectedText.Select ;
            txtticket.Focus();
            LblConse.Text = "C";
        }

        private void txtticket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Size = new Size(865, 542);
                BuscaPendiente("C", txtticket.Text);
                LLenaInfo(Convert.ToInt32(txtticket.Text));
                btnAlta.Enabled = false;
            }
        }

        

        
    }
}
