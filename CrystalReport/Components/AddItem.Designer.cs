using System.Windows.Forms;
namespace CrystalReport.Components
{
   public partial class AddItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

         
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItem));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.good = new System.Windows.Forms.RadioButton();
            this.service = new System.Windows.Forms.RadioButton();
            this.stock = new System.Windows.Forms.TextBox();
            this.unit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.msgdate = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.expdate = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.igst = new System.Windows.Forms.TextBox();
            this.cgst = new System.Windows.Forms.TextBox();
            this.sgst = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.gst = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.Colors = new System.Windows.Forms.TextBox();
            this.size = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.hsn = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.file_name = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.name_item = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.sprice = new System.Windows.Forms.TextBox();
            this.cprice = new System.Windows.Forms.TextBox();
            this.ds = new System.Windows.Forms.RichTextBox();
            this.dss = new System.Windows.Forms.RichTextBox();
            this.ac = new System.Windows.Forms.ComboBox();
            this.acc = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.MRP = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name / (नाव):";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(30, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Stock:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(31, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Unit:";
            // 
            // good
            // 
            this.good.AutoSize = true;
            this.good.Location = new System.Drawing.Point(138, 23);
            this.good.Name = "good";
            this.good.Size = new System.Drawing.Size(65, 21);
            this.good.TabIndex = 0;
            this.good.TabStop = true;
            this.good.Text = "Goods";
            this.good.UseVisualStyleBackColor = true;
            // 
            // service
            // 
            this.service.AutoSize = true;
            this.service.Location = new System.Drawing.Point(211, 22);
            this.service.Name = "service";
            this.service.Size = new System.Drawing.Size(69, 21);
            this.service.TabIndex = 1;
            this.service.TabStop = true;
            this.service.Text = "Service";
            this.service.UseVisualStyleBackColor = true;
            // 
            // stock
            // 
            this.stock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.stock.Location = new System.Drawing.Point(137, 132);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(233, 26);
            this.stock.TabIndex = 1;
            this.stock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // unit
            // 
            this.unit.BackColor = System.Drawing.Color.White;
            this.unit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.unit.FormattingEnabled = true;
            this.unit.ItemHeight = 20;
            this.unit.Location = new System.Drawing.Point(137, 167);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(233, 28);
            this.unit.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Barcode";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Window;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(31, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "Type:";
            this.label12.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 10000000;
            this.label1.Text = "Product Entry";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(63)))), ((int)(((byte)(85)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.Color.Crimson;
            this.panel1.Location = new System.Drawing.Point(-8, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(988, 36);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(814, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(86, 31);
            this.textBox1.TabIndex = 28;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(710, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 25);
            this.label13.TabIndex = 30;
            this.label13.Text = "No.Stocks";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.ForeColor = System.Drawing.Color.Crimson;
            this.panel3.Location = new System.Drawing.Point(-2, 682);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(951, 40);
            this.panel3.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::CrystalReport.Properties.Resources.diskette;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(4, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 29);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add item (F1)";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button1_KeyPress);
            this.button1.Leave += new System.EventHandler(this.button1_Leave);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(905, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 30);
            this.button3.TabIndex = 29;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = global::CrystalReport.Properties.Resources.eraser;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(134, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 29);
            this.button2.TabIndex = 14;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.igst);
            this.panel4.Controls.Add(this.cgst);
            this.panel4.Controls.Add(this.sgst);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.pictureBox4);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Controls.Add(this.gst);
            this.panel4.Controls.Add(this.label21);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.hsn);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.file_name);
            this.panel4.Controls.Add(this.checkBox3);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.good);
            this.panel4.Controls.Add(this.service);
            this.panel4.Controls.Add(this.name_item);
            this.panel4.Controls.Add(this.unit);
            this.panel4.Controls.Add(this.stock);
            this.panel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(-1, 66);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(944, 366);
            this.panel4.TabIndex = 13;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label29);
            this.panel10.Controls.Add(this.label28);
            this.panel10.Controls.Add(this.msgdate);
            this.panel10.Controls.Add(this.label26);
            this.panel10.Controls.Add(this.expdate);
            this.panel10.Controls.Add(this.label27);
            this.panel10.Location = new System.Drawing.Point(138, 313);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(425, 51);
            this.panel10.TabIndex = 43;
            this.panel10.Visible = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(211, 14);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(34, 21);
            this.label29.TabIndex = 45;
            this.label29.Text = "Exp";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(4, 14);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(43, 21);
            this.label28.TabIndex = 44;
            this.label28.Text = "Msg:";
            // 
            // msgdate
            // 
            this.msgdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.msgdate.Location = new System.Drawing.Point(52, 14);
            this.msgdate.Name = "msgdate";
            this.msgdate.Size = new System.Drawing.Size(153, 25);
            this.msgdate.TabIndex = 39;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.SystemColors.Window;
            this.label26.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(-60, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 21);
            this.label26.TabIndex = 29;
            this.label26.Text = "Msg:";
            // 
            // expdate
            // 
            this.expdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expdate.Location = new System.Drawing.Point(250, 12);
            this.expdate.Name = "expdate";
            this.expdate.Size = new System.Drawing.Size(153, 25);
            this.expdate.TabIndex = 41;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.SystemColors.Window;
            this.label27.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(149, 15);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(34, 21);
            this.label27.TabIndex = 40;
            this.label27.Text = "Exp";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(558, 216);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 21);
            this.checkBox1.TabIndex = 42;
            this.checkBox1.Text = "Info";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // igst
            // 
            this.igst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.igst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.igst.Location = new System.Drawing.Point(700, 272);
            this.igst.Name = "igst";
            this.igst.Size = new System.Drawing.Size(87, 26);
            this.igst.TabIndex = 8;
            this.igst.Text = "0";
            this.igst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cgst
            // 
            this.cgst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cgst.Location = new System.Drawing.Point(540, 274);
            this.cgst.Name = "cgst";
            this.cgst.Size = new System.Drawing.Size(87, 26);
            this.cgst.TabIndex = 7;
            this.cgst.Text = "0";
            this.cgst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sgst
            // 
            this.sgst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sgst.Location = new System.Drawing.Point(349, 277);
            this.sgst.Name = "sgst";
            this.sgst.Size = new System.Drawing.Size(87, 26);
            this.sgst.TabIndex = 6;
            this.sgst.Text = "0";
            this.sgst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(636, 276);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 17);
            this.label24.TabIndex = 34;
            this.label24.Text = "IGST %";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(442, 278);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(54, 17);
            this.label23.TabIndex = 33;
            this.label23.Text = "CGST %";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(279, 279);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 17);
            this.label22.TabIndex = 32;
            this.label22.Text = "SGST %";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::CrystalReport.Properties.Resources.add64;
            this.pictureBox4.Location = new System.Drawing.Point(237, 278);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 23);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 31;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::CrystalReport.Properties.Resources.add64;
            this.pictureBox3.Location = new System.Drawing.Point(376, 169);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 23);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 30;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // gst
            // 
            this.gst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.gst.FormattingEnabled = true;
            this.gst.Items.AddRange(new object[] {
            "0%",
            "2.5%",
            "5%",
            "12%",
            "18%",
            "28%"});
            this.gst.Location = new System.Drawing.Point(137, 276);
            this.gst.Name = "gst";
            this.gst.Size = new System.Drawing.Size(94, 28);
            this.gst.TabIndex = 5;
            this.gst.SelectedIndexChanged += new System.EventHandler(this.gst_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(32, 282);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 17);
            this.label21.TabIndex = 28;
            this.label21.Text = "GST";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.Colors);
            this.panel6.Controls.Add(this.size);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Location = new System.Drawing.Point(554, 108);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(384, 94);
            this.panel6.TabIndex = 27;
            // 
            // Colors
            // 
            this.Colors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Colors.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Colors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Colors.Location = new System.Drawing.Point(74, 57);
            this.Colors.Name = "Colors";
            this.Colors.Size = new System.Drawing.Size(288, 26);
            this.Colors.TabIndex = 28;
            // 
            // size
            // 
            this.size.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.size.BackColor = System.Drawing.Color.WhiteSmoke;
            this.size.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.size.Location = new System.Drawing.Point(74, 15);
            this.size.Name = "size";
            this.size.Size = new System.Drawing.Size(288, 26);
            this.size.TabIndex = 28;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Window;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(11, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 17);
            this.label20.TabIndex = 28;
            this.label20.Text = "Color:";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Window;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(11, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 17);
            this.label19.TabIndex = 28;
            this.label19.Text = "Size:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.Window;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(31, 203);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(69, 17);
            this.label18.TabIndex = 25;
            this.label18.Text = "HSN Code";
            // 
            // hsn
            // 
            this.hsn.BackColor = System.Drawing.Color.White;
            this.hsn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.hsn.Location = new System.Drawing.Point(138, 201);
            this.hsn.Name = "hsn";
            this.hsn.Size = new System.Drawing.Size(233, 26);
            this.hsn.TabIndex = 3;
            this.hsn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.Window;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(30, 65);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 17);
            this.label15.TabIndex = 24;
            this.label15.Text = "Import Data:";
            // 
            // file_name
            // 
            this.file_name.AutoSize = true;
            this.file_name.BackColor = System.Drawing.SystemColors.Window;
            this.file_name.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_name.ForeColor = System.Drawing.Color.Red;
            this.file_name.Location = new System.Drawing.Point(210, 63);
            this.file_name.Name = "file_name";
            this.file_name.Size = new System.Drawing.Size(111, 17);
            this.file_name.TabIndex = 11;
            this.file_name.Text = "Select Excel Sheet";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(376, 246);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(125, 21);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Manual Barcode";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CrystalReport.Properties.Resources.excel;
            this.pictureBox1.Location = new System.Drawing.Point(138, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.textBox2.Location = new System.Drawing.Point(137, 241);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(233, 26);
            this.textBox2.TabIndex = 4;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(613, 23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(284, 63);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // name_item
            // 
            this.name_item.BackColor = System.Drawing.SystemColors.Control;
            this.name_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.name_item.FormattingEnabled = true;
            this.name_item.Location = new System.Drawing.Point(137, 95);
            this.name_item.Name = "name_item";
            this.name_item.Size = new System.Drawing.Size(328, 28);
            this.name_item.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Sale Price:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Account:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(2, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "Account:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Description";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(2, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 17);
            this.label11.TabIndex = 1;
            this.label11.Text = "Description";
            // 
            // sprice
            // 
            this.sprice.BackColor = System.Drawing.Color.White;
            this.sprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sprice.Location = new System.Drawing.Point(202, 14);
            this.sprice.Name = "sprice";
            this.sprice.Size = new System.Drawing.Size(286, 26);
            this.sprice.TabIndex = 9;
            this.sprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cprice
            // 
            this.cprice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cprice.BackColor = System.Drawing.Color.White;
            this.cprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cprice.Location = new System.Drawing.Point(99, 10);
            this.cprice.Name = "cprice";
            this.cprice.Size = new System.Drawing.Size(316, 26);
            this.cprice.TabIndex = 12;
            this.cprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ds
            // 
            this.ds.BackColor = System.Drawing.SystemColors.Control;
            this.ds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ds.Location = new System.Drawing.Point(203, 96);
            this.ds.Name = "ds";
            this.ds.Size = new System.Drawing.Size(286, 60);
            this.ds.TabIndex = 11;
            this.ds.Text = "";
            // 
            // dss
            // 
            this.dss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dss.BackColor = System.Drawing.SystemColors.Control;
            this.dss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dss.Location = new System.Drawing.Point(98, 113);
            this.dss.Name = "dss";
            this.dss.Size = new System.Drawing.Size(316, 56);
            this.dss.TabIndex = 15;
            this.dss.Text = "";
            // 
            // ac
            // 
            this.ac.BackColor = System.Drawing.Color.White;
            this.ac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ac.FormattingEnabled = true;
            this.ac.Location = new System.Drawing.Point(203, 59);
            this.ac.Name = "ac";
            this.ac.Size = new System.Drawing.Size(286, 28);
            this.ac.TabIndex = 10;
            // 
            // acc
            // 
            this.acc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.acc.BackColor = System.Drawing.SystemColors.Control;
            this.acc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.acc.FormattingEnabled = true;
            this.acc.Location = new System.Drawing.Point(98, 77);
            this.acc.Name = "acc";
            this.acc.Size = new System.Drawing.Size(316, 28);
            this.acc.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 17);
            this.label14.TabIndex = 1;
            this.label14.Text = "Cost Price:";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(-2, 35);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(948, 31);
            this.panel5.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 21);
            this.label9.TabIndex = 5;
            this.label9.Text = "Item Details";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(3, 4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 21);
            this.label16.TabIndex = 5;
            this.label16.Text = "Selling Info";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label16);
            this.panel2.Location = new System.Drawing.Point(1, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 31);
            this.panel2.TabIndex = 17;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label17);
            this.panel7.Location = new System.Drawing.Point(517, 443);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(420, 31);
            this.panel7.TabIndex = 18;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(3, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 21);
            this.label17.TabIndex = 5;
            this.label17.Text = "Purches Info";
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.sprice);
            this.panel8.Controls.Add(this.ac);
            this.panel8.Controls.Add(this.ds);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Location = new System.Drawing.Point(1, 474);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(498, 174);
            this.panel8.TabIndex = 19;
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.MRP);
            this.panel9.Controls.Add(this.label25);
            this.panel9.Controls.Add(this.cprice);
            this.panel9.Controls.Add(this.dss);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.acc);
            this.panel9.Controls.Add(this.label10);
            this.panel9.Controls.Add(this.label14);
            this.panel9.Location = new System.Drawing.Point(518, 474);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(420, 174);
            this.panel9.TabIndex = 20;
            // 
            // MRP
            // 
            this.MRP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MRP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MRP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MRP.Location = new System.Drawing.Point(99, 42);
            this.MRP.Name = "MRP";
            this.MRP.Size = new System.Drawing.Size(316, 26);
            this.MRP.TabIndex = 13;
            this.MRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(4, 43);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(38, 17);
            this.label25.TabIndex = 11;
            this.label25.Text = "MRP:";
            // 
            // AddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "AddItem";
            this.Size = new System.Drawing.Size(949, 721);
            this.Load += new System.EventHandler(this.AddItem_Load);
            this.Enter += new System.EventHandler(this.AddItem_Enter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddItem_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label3;
        private Label label4;
        private Label label6;
        private RadioButton good;
        private RadioButton service;
        private TextBox stock;
        private ComboBox unit;
        private Label label2;
        private Label label12;
        private Label label1;
        private Panel panel1;
        private Button button1;
        private Button button3;
        private Button button2;
        private TextBox textBox1;
        private Label label13;
        private Panel panel3;
        private Panel panel4;
        private ComboBox name_item;
        private Label label5;
        private Label label7;
        private Label label10;
        private Label label8;
        private Label label11;
        private TextBox sprice;
        private TextBox cprice;
        private RichTextBox ds;
        private RichTextBox dss;
        private ComboBox ac;
        private ComboBox acc;
        private Label label14;
        private PictureBox pictureBox2;
        private TextBox textBox2;
        private CheckBox checkBox3;
        private Panel panel5;
        private Label label9;
        private Label label16;
        private Panel panel2;
        private Panel panel7;
        private Label label17;
        private Panel panel8;
        private Panel panel9;
        private PictureBox pictureBox1;
        private Label file_name;
        private Label label15;
        private Label label18;
        private TextBox hsn;
        private Panel panel6;
        private Label label20;
        private Label label19;
        private TextBox Colors;
        private TextBox size;
        private ComboBox gst;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label24;
        private Label label23;
        private Label label22;
        private Label label21;
        private TextBox cgst;
        private TextBox sgst;
        private TextBox igst;
        private TextBox MRP;
        private Label label25;
        private DateTimePicker msgdate;
        private Label label26;
        private Panel panel10;
        private DateTimePicker expdate;
        private Label label27;
        private CheckBox checkBox1;
        private Label label29;
        private Label label28;
    }
}
