namespace Recepcion_Bascula
{
    partial class FrmTicketBascula
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTicketBascula));
            this.LbxTick = new System.Windows.Forms.ListBox();
            this.lbfletes = new System.Windows.Forms.Label();
            this.btnAlta = new System.Windows.Forms.Button();
            this.txtticket = new System.Windows.Forms.TextBox();
            this.lbticket = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblPesoBas = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DtFec = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LblHora = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbTipo = new System.Windows.Forms.ComboBox();
            this.DGDatos = new System.Windows.Forms.DataGridView();
            this.FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WEIGHT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GROSS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TARE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NET = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbNombre = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txttrans = new System.Windows.Forms.TextBox();
            this.lbtransportista = new System.Windows.Forms.Label();
            this.TxtPla = new System.Windows.Forms.TextBox();
            this.LblConse = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.LblEje1 = new System.Windows.Forms.Label();
            this.LblEje12 = new System.Windows.Forms.Label();
            this.LblEje3 = new System.Windows.Forms.Label();
            this.LblEje123 = new System.Windows.Forms.Label();
            this.LblPenE12 = new System.Windows.Forms.Label();
            this.LblPenE3 = new System.Windows.Forms.Label();
            this.LblPenE123 = new System.Windows.Forms.Label();
            this.PdTicket = new System.Drawing.Printing.PrintDocument();
            this.PbxLogoGab = new System.Windows.Forms.PictureBox();
            this.PbxLogoMr = new System.Windows.Forms.PictureBox();
            this.PbxSel = new System.Windows.Forms.PictureBox();
            this.PbxEje123 = new System.Windows.Forms.PictureBox();
            this.PbxEje3 = new System.Windows.Forms.PictureBox();
            this.PbxEje12 = new System.Windows.Forms.PictureBox();
            this.PbxEje1 = new System.Windows.Forms.PictureBox();
            this.BtnImp = new System.Windows.Forms.Button();
            this.PdTicketExp = new System.Drawing.Printing.PrintDocument();
            this.PbxQR = new System.Windows.Forms.PictureBox();
            this.BtnTic = new System.Windows.Forms.Button();
            this.LblPenE1 = new System.Windows.Forms.Label();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtChofer = new System.Windows.Forms.TextBox();
            this.TxtRan = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtProd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ChkCampo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxLogoGab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxLogoMr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxSel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje123)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxQR)).BeginInit();
            this.SuspendLayout();
            // 
            // LbxTick
            // 
            this.LbxTick.FormattingEnabled = true;
            this.LbxTick.Location = new System.Drawing.Point(12, 133);
            this.LbxTick.Name = "LbxTick";
            this.LbxTick.Size = new System.Drawing.Size(178, 420);
            this.LbxTick.TabIndex = 0;
            this.LbxTick.DoubleClick += new System.EventHandler(this.LbxTick_DoubleClick);
            // 
            // lbfletes
            // 
            this.lbfletes.AutoSize = true;
            this.lbfletes.BackColor = System.Drawing.Color.Transparent;
            this.lbfletes.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbfletes.ForeColor = System.Drawing.Color.White;
            this.lbfletes.Location = new System.Drawing.Point(36, 114);
            this.lbfletes.Name = "lbfletes";
            this.lbfletes.Size = new System.Drawing.Size(111, 16);
            this.lbfletes.TabIndex = 312;
            this.lbfletes.Text = "Tickets Abiertos";
            // 
            // btnAlta
            // 
            this.btnAlta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAlta.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlta.ForeColor = System.Drawing.Color.Black;
            this.btnAlta.Location = new System.Drawing.Point(211, 84);
            this.btnAlta.Name = "btnAlta";
            this.btnAlta.Size = new System.Drawing.Size(86, 27);
            this.btnAlta.TabIndex = 313;
            this.btnAlta.Text = "ALTA";
            this.btnAlta.UseVisualStyleBackColor = false;
            this.btnAlta.Click += new System.EventHandler(this.btnAlta_Click);
            // 
            // txtticket
            // 
            this.txtticket.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtticket.Enabled = false;
            this.txtticket.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtticket.ForeColor = System.Drawing.Color.Black;
            this.txtticket.Location = new System.Drawing.Point(272, 124);
            this.txtticket.MaxLength = 10;
            this.txtticket.Name = "txtticket";
            this.txtticket.Size = new System.Drawing.Size(102, 29);
            this.txtticket.TabIndex = 316;
            this.txtticket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtticket_KeyPress);
            // 
            // lbticket
            // 
            this.lbticket.AutoSize = true;
            this.lbticket.BackColor = System.Drawing.Color.Transparent;
            this.lbticket.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbticket.ForeColor = System.Drawing.Color.White;
            this.lbticket.Location = new System.Drawing.Point(213, 133);
            this.lbticket.Name = "lbticket";
            this.lbticket.Size = new System.Drawing.Size(51, 16);
            this.lbticket.TabIndex = 315;
            this.lbticket.Text = "Ticket:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 366;
            this.label2.Text = "Peso Bascula";
            // 
            // LblPesoBas
            // 
            this.LblPesoBas.AutoSize = true;
            this.LblPesoBas.BackColor = System.Drawing.Color.Transparent;
            this.LblPesoBas.Font = new System.Drawing.Font("Lucida Sans Unicode", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPesoBas.ForeColor = System.Drawing.Color.Yellow;
            this.LblPesoBas.Location = new System.Drawing.Point(6, 45);
            this.LblPesoBas.Name = "LblPesoBas";
            this.LblPesoBas.Size = new System.Drawing.Size(193, 37);
            this.LblPesoBas.TabIndex = 365;
            this.LblPesoBas.Text = "PesoBascula";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(445, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 367;
            this.label1.Text = "Fecha:";
            // 
            // DtFec
            // 
            this.DtFec.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtFec.Location = new System.Drawing.Point(499, 133);
            this.DtFec.Name = "DtFec";
            this.DtFec.Size = new System.Drawing.Size(100, 20);
            this.DtFec.TabIndex = 368;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LblHora
            // 
            this.LblHora.AutoSize = true;
            this.LblHora.BackColor = System.Drawing.Color.Transparent;
            this.LblHora.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHora.ForeColor = System.Drawing.Color.Yellow;
            this.LblHora.Location = new System.Drawing.Point(500, 109);
            this.LblHora.Name = "LblHora";
            this.LblHora.Size = new System.Drawing.Size(47, 20);
            this.LblHora.TabIndex = 369;
            this.LblHora.Text = "Hora";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(213, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 370;
            this.label3.Text = "Tipo:";
            // 
            // CmbTipo
            // 
            this.CmbTipo.Enabled = false;
            this.CmbTipo.FormattingEnabled = true;
            this.CmbTipo.Items.AddRange(new object[] {
            "NACIONAL",
            "EXPORTACION"});
            this.CmbTipo.Location = new System.Drawing.Point(272, 169);
            this.CmbTipo.Name = "CmbTipo";
            this.CmbTipo.Size = new System.Drawing.Size(121, 21);
            this.CmbTipo.TabIndex = 371;
            // 
            // DGDatos
            // 
            this.DGDatos.AllowUserToAddRows = false;
            this.DGDatos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FECHA,
            this.hora,
            this.WEIGHT,
            this.GROSS,
            this.TARE,
            this.NET});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGDatos.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGDatos.Location = new System.Drawing.Point(211, 406);
            this.DGDatos.Name = "DGDatos";
            this.DGDatos.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGDatos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGDatos.Size = new System.Drawing.Size(612, 155);
            this.DGDatos.TabIndex = 372;
            // 
            // FECHA
            // 
            this.FECHA.HeaderText = "FECHA";
            this.FECHA.Name = "FECHA";
            this.FECHA.ReadOnly = true;
            this.FECHA.Width = 90;
            // 
            // hora
            // 
            this.hora.HeaderText = "HORA";
            this.hora.Name = "hora";
            this.hora.ReadOnly = true;
            this.hora.Width = 90;
            // 
            // WEIGHT
            // 
            this.WEIGHT.HeaderText = "WEIGHT";
            this.WEIGHT.Name = "WEIGHT";
            this.WEIGHT.ReadOnly = true;
            this.WEIGHT.Width = 90;
            // 
            // GROSS
            // 
            this.GROSS.HeaderText = "GROSS";
            this.GROSS.Name = "GROSS";
            this.GROSS.ReadOnly = true;
            this.GROSS.Width = 90;
            // 
            // TARE
            // 
            this.TARE.HeaderText = "TARE";
            this.TARE.Name = "TARE";
            this.TARE.ReadOnly = true;
            this.TARE.Width = 90;
            // 
            // NET
            // 
            this.NET.HeaderText = "NET";
            this.NET.Name = "NET";
            this.NET.ReadOnly = true;
            this.NET.Width = 90;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Location = new System.Drawing.Point(732, 84);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(88, 27);
            this.btnGuardar.TabIndex = 373;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(213, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 374;
            this.label4.Text = "Nombre:";
            // 
            // CmbNombre
            // 
            this.CmbNombre.Enabled = false;
            this.CmbNombre.FormattingEnabled = true;
            this.CmbNombre.Location = new System.Drawing.Point(279, 204);
            this.CmbNombre.Name = "CmbNombre";
            this.CmbNombre.Size = new System.Drawing.Size(260, 21);
            this.CmbNombre.TabIndex = 375;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(548, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 376;
            this.label5.Text = "Placa:";
            // 
            // txttrans
            // 
            this.txttrans.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txttrans.Enabled = false;
            this.txttrans.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttrans.ForeColor = System.Drawing.Color.Black;
            this.txttrans.Location = new System.Drawing.Point(313, 244);
            this.txttrans.MaxLength = 100;
            this.txttrans.Name = "txttrans";
            this.txttrans.Size = new System.Drawing.Size(217, 20);
            this.txttrans.TabIndex = 378;
            // 
            // lbtransportista
            // 
            this.lbtransportista.AutoSize = true;
            this.lbtransportista.BackColor = System.Drawing.Color.Transparent;
            this.lbtransportista.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtransportista.ForeColor = System.Drawing.Color.Black;
            this.lbtransportista.Location = new System.Drawing.Point(211, 247);
            this.lbtransportista.Name = "lbtransportista";
            this.lbtransportista.Size = new System.Drawing.Size(96, 16);
            this.lbtransportista.TabIndex = 377;
            this.lbtransportista.Text = "Transportista:";
            // 
            // TxtPla
            // 
            this.TxtPla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPla.Enabled = false;
            this.TxtPla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPla.ForeColor = System.Drawing.Color.Black;
            this.TxtPla.Location = new System.Drawing.Point(614, 244);
            this.TxtPla.MaxLength = 100;
            this.TxtPla.Name = "TxtPla";
            this.TxtPla.Size = new System.Drawing.Size(95, 20);
            this.TxtPla.TabIndex = 379;
            // 
            // LblConse
            // 
            this.LblConse.AutoSize = true;
            this.LblConse.BackColor = System.Drawing.Color.Transparent;
            this.LblConse.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblConse.ForeColor = System.Drawing.Color.Black;
            this.LblConse.Location = new System.Drawing.Point(380, 135);
            this.LblConse.Name = "LblConse";
            this.LblConse.Size = new System.Drawing.Size(16, 16);
            this.LblConse.TabIndex = 380;
            this.LblConse.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(609, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 27);
            this.btnCancel.TabIndex = 381;
            this.btnCancel.Text = "CANCELAR";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(884, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 23);
            this.label6.TabIndex = 382;
            this.label6.Text = "Pesos Por Ejes";
            // 
            // LblEje1
            // 
            this.LblEje1.AutoSize = true;
            this.LblEje1.BackColor = System.Drawing.Color.Transparent;
            this.LblEje1.Font = new System.Drawing.Font("Lucida Sans Unicode", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEje1.ForeColor = System.Drawing.Color.White;
            this.LblEje1.Location = new System.Drawing.Point(860, 206);
            this.LblEje1.Name = "LblEje1";
            this.LblEje1.Size = new System.Drawing.Size(75, 14);
            this.LblEje1.TabIndex = 388;
            this.LblEje1.Text = "Pesos Por Ejes";
            // 
            // LblEje12
            // 
            this.LblEje12.AutoSize = true;
            this.LblEje12.BackColor = System.Drawing.Color.Transparent;
            this.LblEje12.Font = new System.Drawing.Font("Lucida Sans Unicode", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEje12.ForeColor = System.Drawing.Color.Blue;
            this.LblEje12.Location = new System.Drawing.Point(859, 294);
            this.LblEje12.Name = "LblEje12";
            this.LblEje12.Size = new System.Drawing.Size(75, 14);
            this.LblEje12.TabIndex = 389;
            this.LblEje12.Text = "Pesos Por Ejes";
            // 
            // LblEje3
            // 
            this.LblEje3.AutoSize = true;
            this.LblEje3.BackColor = System.Drawing.Color.Transparent;
            this.LblEje3.Font = new System.Drawing.Font("Lucida Sans Unicode", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEje3.ForeColor = System.Drawing.Color.Blue;
            this.LblEje3.Location = new System.Drawing.Point(858, 381);
            this.LblEje3.Name = "LblEje3";
            this.LblEje3.Size = new System.Drawing.Size(75, 14);
            this.LblEje3.TabIndex = 390;
            this.LblEje3.Text = "Pesos Por Ejes";
            // 
            // LblEje123
            // 
            this.LblEje123.AutoSize = true;
            this.LblEje123.BackColor = System.Drawing.Color.Transparent;
            this.LblEje123.Font = new System.Drawing.Font("Lucida Sans Unicode", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEje123.ForeColor = System.Drawing.Color.Blue;
            this.LblEje123.Location = new System.Drawing.Point(860, 466);
            this.LblEje123.Name = "LblEje123";
            this.LblEje123.Size = new System.Drawing.Size(75, 14);
            this.LblEje123.TabIndex = 391;
            this.LblEje123.Text = "Pesos Por Ejes";
            // 
            // LblPenE12
            // 
            this.LblPenE12.AutoSize = true;
            this.LblPenE12.BackColor = System.Drawing.Color.Transparent;
            this.LblPenE12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPenE12.ForeColor = System.Drawing.Color.Red;
            this.LblPenE12.Location = new System.Drawing.Point(933, 250);
            this.LblPenE12.Name = "LblPenE12";
            this.LblPenE12.Size = new System.Drawing.Size(100, 20);
            this.LblPenE12.TabIndex = 393;
            this.LblPenE12.Text = "PENDIENTE";
            // 
            // LblPenE3
            // 
            this.LblPenE3.AutoSize = true;
            this.LblPenE3.BackColor = System.Drawing.Color.Transparent;
            this.LblPenE3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPenE3.ForeColor = System.Drawing.Color.Red;
            this.LblPenE3.Location = new System.Drawing.Point(933, 338);
            this.LblPenE3.Name = "LblPenE3";
            this.LblPenE3.Size = new System.Drawing.Size(100, 20);
            this.LblPenE3.TabIndex = 394;
            this.LblPenE3.Text = "PENDIENTE";
            // 
            // LblPenE123
            // 
            this.LblPenE123.AutoSize = true;
            this.LblPenE123.BackColor = System.Drawing.Color.Transparent;
            this.LblPenE123.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPenE123.ForeColor = System.Drawing.Color.Red;
            this.LblPenE123.Location = new System.Drawing.Point(933, 425);
            this.LblPenE123.Name = "LblPenE123";
            this.LblPenE123.Size = new System.Drawing.Size(100, 20);
            this.LblPenE123.TabIndex = 395;
            this.LblPenE123.Text = "PENDIENTE";
            // 
            // PdTicket
            // 
            this.PdTicket.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PdTicket_PrintPage);
            // 
            // PbxLogoGab
            // 
            this.PbxLogoGab.Image = global::Recepcion_Bascula.Properties.Resources.LOGOGAB;
            this.PbxLogoGab.Location = new System.Drawing.Point(731, 7);
            this.PbxLogoGab.Name = "PbxLogoGab";
            this.PbxLogoGab.Size = new System.Drawing.Size(78, 40);
            this.PbxLogoGab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxLogoGab.TabIndex = 397;
            this.PbxLogoGab.TabStop = false;
            // 
            // PbxLogoMr
            // 
            this.PbxLogoMr.Image = global::Recepcion_Bascula.Properties.Resources.logonegro;
            this.PbxLogoMr.Location = new System.Drawing.Point(658, 7);
            this.PbxLogoMr.Name = "PbxLogoMr";
            this.PbxLogoMr.Size = new System.Drawing.Size(51, 40);
            this.PbxLogoMr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxLogoMr.TabIndex = 396;
            this.PbxLogoMr.TabStop = false;
            // 
            // PbxSel
            // 
            this.PbxSel.BackColor = System.Drawing.Color.Transparent;
            this.PbxSel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxSel.Location = new System.Drawing.Point(636, 124);
            this.PbxSel.Name = "PbxSel";
            this.PbxSel.Size = new System.Drawing.Size(187, 55);
            this.PbxSel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxSel.TabIndex = 387;
            this.PbxSel.TabStop = false;
            // 
            // PbxEje123
            // 
            this.PbxEje123.BackColor = System.Drawing.Color.Transparent;
            this.PbxEje123.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxEje123.Image = global::Recepcion_Bascula.Properties.Resources.Eje123;
            this.PbxEje123.Location = new System.Drawing.Point(862, 410);
            this.PbxEje123.Name = "PbxEje123";
            this.PbxEje123.Size = new System.Drawing.Size(187, 55);
            this.PbxEje123.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxEje123.TabIndex = 386;
            this.PbxEje123.TabStop = false;
            this.PbxEje123.Click += new System.EventHandler(this.PbxEje123_Click);
            // 
            // PbxEje3
            // 
            this.PbxEje3.BackColor = System.Drawing.Color.Transparent;
            this.PbxEje3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxEje3.Image = global::Recepcion_Bascula.Properties.Resources.Eje3;
            this.PbxEje3.Location = new System.Drawing.Point(862, 324);
            this.PbxEje3.Name = "PbxEje3";
            this.PbxEje3.Size = new System.Drawing.Size(187, 55);
            this.PbxEje3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxEje3.TabIndex = 385;
            this.PbxEje3.TabStop = false;
            this.PbxEje3.Click += new System.EventHandler(this.PbxEje3_Click);
            // 
            // PbxEje12
            // 
            this.PbxEje12.BackColor = System.Drawing.Color.Transparent;
            this.PbxEje12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxEje12.Image = global::Recepcion_Bascula.Properties.Resources.Eje1y2;
            this.PbxEje12.Location = new System.Drawing.Point(862, 236);
            this.PbxEje12.Name = "PbxEje12";
            this.PbxEje12.Size = new System.Drawing.Size(187, 55);
            this.PbxEje12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxEje12.TabIndex = 384;
            this.PbxEje12.TabStop = false;
            this.PbxEje12.Click += new System.EventHandler(this.PbxEje12_Click);
            // 
            // PbxEje1
            // 
            this.PbxEje1.BackColor = System.Drawing.Color.Transparent;
            this.PbxEje1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PbxEje1.Image = global::Recepcion_Bascula.Properties.Resources.Eje1;
            this.PbxEje1.Location = new System.Drawing.Point(862, 149);
            this.PbxEje1.Name = "PbxEje1";
            this.PbxEje1.Size = new System.Drawing.Size(187, 55);
            this.PbxEje1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxEje1.TabIndex = 383;
            this.PbxEje1.TabStop = false;
            this.PbxEje1.Click += new System.EventHandler(this.PbxEje1_Click);
            // 
            // BtnImp
            // 
            this.BtnImp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BtnImp.Enabled = false;
            this.BtnImp.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImp.ForeColor = System.Drawing.Color.Black;
            this.BtnImp.Image = global::Recepcion_Bascula.Properties.Resources.ImprimirMini;
            this.BtnImp.Location = new System.Drawing.Point(765, 347);
            this.BtnImp.Name = "BtnImp";
            this.BtnImp.Size = new System.Drawing.Size(58, 46);
            this.BtnImp.TabIndex = 314;
            this.BtnImp.UseVisualStyleBackColor = false;
            this.BtnImp.Click += new System.EventHandler(this.BtnImp_Click);
            // 
            // PdTicketExp
            // 
            this.PdTicketExp.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PdTicketExp_PrintPage);
            // 
            // PbxQR
            // 
            this.PbxQR.Location = new System.Drawing.Point(937, 74);
            this.PbxQR.Name = "PbxQR";
            this.PbxQR.Size = new System.Drawing.Size(38, 37);
            this.PbxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxQR.TabIndex = 398;
            this.PbxQR.TabStop = false;
            // 
            // BtnTic
            // 
            this.BtnTic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BtnTic.Enabled = false;
            this.BtnTic.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTic.ForeColor = System.Drawing.Color.Black;
            this.BtnTic.Image = global::Recepcion_Bascula.Properties.Resources.ImprimirMini;
            this.BtnTic.Location = new System.Drawing.Point(701, 347);
            this.BtnTic.Name = "BtnTic";
            this.BtnTic.Size = new System.Drawing.Size(58, 46);
            this.BtnTic.TabIndex = 399;
            this.BtnTic.UseVisualStyleBackColor = false;
            this.BtnTic.Click += new System.EventHandler(this.BtnTic_Click);
            // 
            // LblPenE1
            // 
            this.LblPenE1.AutoSize = true;
            this.LblPenE1.BackColor = System.Drawing.Color.Transparent;
            this.LblPenE1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPenE1.ForeColor = System.Drawing.Color.Red;
            this.LblPenE1.Location = new System.Drawing.Point(933, 163);
            this.LblPenE1.Name = "LblPenE1";
            this.LblPenE1.Size = new System.Drawing.Size(100, 20);
            this.LblPenE1.TabIndex = 400;
            this.LblPenE1.Text = "PENDIENTE";
            // 
            // btnConsulta
            // 
            this.btnConsulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnConsulta.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsulta.ForeColor = System.Drawing.Color.Black;
            this.btnConsulta.Location = new System.Drawing.Point(353, 84);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(98, 27);
            this.btnConsulta.TabIndex = 401;
            this.btnConsulta.Text = "CONSULTA";
            this.btnConsulta.UseVisualStyleBackColor = false;
            this.btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(211, 294);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 16);
            this.label7.TabIndex = 402;
            this.label7.Text = "Chofer:";
            // 
            // TxtChofer
            // 
            this.TxtChofer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtChofer.Enabled = false;
            this.TxtChofer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChofer.ForeColor = System.Drawing.Color.Black;
            this.TxtChofer.Location = new System.Drawing.Point(281, 294);
            this.TxtChofer.MaxLength = 100;
            this.TxtChofer.Name = "TxtChofer";
            this.TxtChofer.Size = new System.Drawing.Size(294, 20);
            this.TxtChofer.TabIndex = 403;
            // 
            // TxtRan
            // 
            this.TxtRan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtRan.Enabled = false;
            this.TxtRan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRan.ForeColor = System.Drawing.Color.Black;
            this.TxtRan.Location = new System.Drawing.Point(281, 327);
            this.TxtRan.MaxLength = 100;
            this.TxtRan.Name = "TxtRan";
            this.TxtRan.Size = new System.Drawing.Size(294, 20);
            this.TxtRan.TabIndex = 405;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(211, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 16);
            this.label8.TabIndex = 404;
            this.label8.Text = "Rancho:";
            // 
            // TxtProd
            // 
            this.TxtProd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtProd.Enabled = false;
            this.TxtProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProd.ForeColor = System.Drawing.Color.Black;
            this.TxtProd.Location = new System.Drawing.Point(281, 361);
            this.TxtProd.MaxLength = 100;
            this.TxtProd.Name = "TxtProd";
            this.TxtProd.Size = new System.Drawing.Size(294, 20);
            this.TxtProd.TabIndex = 407;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(211, 361);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 16);
            this.label9.TabIndex = 406;
            this.label9.Text = "Producto:";
            // 
            // ChkCampo
            // 
            this.ChkCampo.AutoSize = true;
            this.ChkCampo.BackColor = System.Drawing.Color.Transparent;
            this.ChkCampo.Enabled = false;
            this.ChkCampo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCampo.ForeColor = System.Drawing.Color.Cyan;
            this.ChkCampo.Location = new System.Drawing.Point(425, 171);
            this.ChkCampo.Name = "ChkCampo";
            this.ChkCampo.Size = new System.Drawing.Size(88, 21);
            this.ChkCampo.TabIndex = 408;
            this.ChkCampo.Text = "CAMPO ?";
            this.ChkCampo.UseVisualStyleBackColor = false;
            // 
            // FrmTicketBascula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1059, 566);
            this.Controls.Add(this.ChkCampo);
            this.Controls.Add(this.TxtProd);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TxtRan);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TxtChofer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnConsulta);
            this.Controls.Add(this.LblPenE1);
            this.Controls.Add(this.BtnTic);
            this.Controls.Add(this.PbxQR);
            this.Controls.Add(this.PbxLogoGab);
            this.Controls.Add(this.PbxLogoMr);
            this.Controls.Add(this.LblPenE123);
            this.Controls.Add(this.LblPenE3);
            this.Controls.Add(this.LblPenE12);
            this.Controls.Add(this.LblEje123);
            this.Controls.Add(this.LblEje3);
            this.Controls.Add(this.LblEje12);
            this.Controls.Add(this.LblEje1);
            this.Controls.Add(this.PbxSel);
            this.Controls.Add(this.PbxEje123);
            this.Controls.Add(this.PbxEje3);
            this.Controls.Add(this.PbxEje12);
            this.Controls.Add(this.PbxEje1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.LblConse);
            this.Controls.Add(this.TxtPla);
            this.Controls.Add(this.txttrans);
            this.Controls.Add(this.lbtransportista);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CmbNombre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.DGDatos);
            this.Controls.Add(this.CmbTipo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LblHora);
            this.Controls.Add(this.DtFec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblPesoBas);
            this.Controls.Add(this.txtticket);
            this.Controls.Add(this.lbticket);
            this.Controls.Add(this.BtnImp);
            this.Controls.Add(this.btnAlta);
            this.Controls.Add(this.lbfletes);
            this.Controls.Add(this.LbxTick);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTicketBascula";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tickets de Bascula";
            this.Load += new System.EventHandler(this.FrmTicketBascula_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxLogoGab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxLogoMr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxSel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje123)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxEje1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxQR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LbxTick;
        private System.Windows.Forms.Label lbfletes;
        private System.Windows.Forms.Button btnAlta;
        private System.Windows.Forms.Button BtnImp;
        private System.Windows.Forms.TextBox txtticket;
        private System.Windows.Forms.Label lbticket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblPesoBas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DtFec;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label LblHora;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CmbTipo;
        private System.Windows.Forms.DataGridView DGDatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn hora;
        private System.Windows.Forms.DataGridViewTextBoxColumn WEIGHT;
        private System.Windows.Forms.DataGridViewTextBoxColumn GROSS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TARE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NET;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbNombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txttrans;
        private System.Windows.Forms.Label lbtransportista;
        private System.Windows.Forms.TextBox TxtPla;
        private System.Windows.Forms.Label LblConse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox PbxEje1;
        private System.Windows.Forms.PictureBox PbxEje12;
        private System.Windows.Forms.PictureBox PbxEje3;
        private System.Windows.Forms.PictureBox PbxEje123;
        private System.Windows.Forms.PictureBox PbxSel;
        private System.Windows.Forms.Label LblEje1;
        private System.Windows.Forms.Label LblEje12;
        private System.Windows.Forms.Label LblEje3;
        private System.Windows.Forms.Label LblEje123;
        private System.Windows.Forms.Label LblPenE12;
        private System.Windows.Forms.Label LblPenE3;
        private System.Windows.Forms.Label LblPenE123;
        private System.Drawing.Printing.PrintDocument PdTicket;
        private System.Windows.Forms.PictureBox PbxLogoMr;
        private System.Windows.Forms.PictureBox PbxLogoGab;
        private System.Drawing.Printing.PrintDocument PdTicketExp;
        private System.Windows.Forms.PictureBox PbxQR;
        private System.Windows.Forms.Button BtnTic;
        private System.Windows.Forms.Label LblPenE1;
        private System.Windows.Forms.Button btnConsulta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtChofer;
        private System.Windows.Forms.TextBox TxtRan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtProd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ChkCampo;
    }
}