namespace Do_an.Forms
{
    partial class UC_Message
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer;

        // LEFT
        private System.Windows.Forms.Panel pnlLeftHeader;
        private System.Windows.Forms.Label lblTitle;
        private Do_an.Forms.RoundedButton btnAddFriend;
        private Do_an.Forms.RoundedButton btnCreateGroup;
        private Do_an.Forms.RoundedButton btnAccept;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flChatList;
        private System.Windows.Forms.Button btnMenu;

        // RIGHT
        private System.Windows.Forms.Panel pnlRightHeader;
        private Do_an.Forms.CircularPictureBox picChatAvatar;
        private System.Windows.Forms.Label lblChatName;
        private System.Windows.Forms.Label lblChatStatus;

        // NÚT CHỨC NĂNG RIGHT HEADER
        private System.Windows.Forms.Button btnMoreInfo; // Nút Xóa
        private System.Windows.Forms.Button btnVoiceCall; // Gọi thường
        private System.Windows.Forms.Button btnVideoCall; // Gọi Video

        private System.Windows.Forms.FlowLayoutPanel flMessages;
        private System.Windows.Forms.Panel pnlInputArea;
        private System.Windows.Forms.Panel pnlTxtContainer;
        private System.Windows.Forms.Panel pnlTxtWrapper;
        private System.Windows.Forms.Panel pnlLeftActions;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnEmoji;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnImage;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            // MÀU SẮC CHỦ ĐẠO (THEME VINTAGE)
            System.Drawing.Color clrWoodDark = System.Drawing.Color.FromArgb(61, 43, 31); // Gỗ tối (Header)
            System.Drawing.Color clrWoodLight = System.Drawing.Color.FromArgb(101, 67, 33); // Gỗ sáng (Button)
            System.Drawing.Color clrPaper = System.Drawing.Color.FromArgb(218, 200, 162); // Giấy cũ (Input)
            System.Drawing.Color clrBookshelf = System.Drawing.Color.FromArgb(40, 25, 15); // Nền danh sách chat
            System.Drawing.Color clrRoom = System.Drawing.Color.FromArgb(200, 190, 170); // Nền chat
            System.Drawing.Color clrTextLight = System.Drawing.Color.FromArgb(240, 230, 210); // Chữ sáng

            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlLeftHeader = new System.Windows.Forms.Panel();
            this.btnMenu = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddFriend = new Do_an.Forms.RoundedButton();
            this.btnCreateGroup = new Do_an.Forms.RoundedButton();
            this.btnAccept = new Do_an.Forms.RoundedButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.flChatList = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRightHeader = new System.Windows.Forms.Panel();
            this.picChatAvatar = new Do_an.Forms.CircularPictureBox();
            this.lblChatName = new System.Windows.Forms.Label();
            this.lblChatStatus = new System.Windows.Forms.Label();

            // Init các nút Header Phải
            this.btnMoreInfo = new System.Windows.Forms.Button();
            this.btnVoiceCall = new System.Windows.Forms.Button();
            this.btnVideoCall = new System.Windows.Forms.Button();

            this.flMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlInputArea = new System.Windows.Forms.Panel();
            this.pnlTxtContainer = new System.Windows.Forms.Panel();
            this.pnlTxtWrapper = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlLeftActions = new System.Windows.Forms.Panel();
            this.btnEmoji = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.btnImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlLeftHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlRightHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChatAvatar)).BeginInit();
            this.pnlInputArea.SuspendLayout();
            this.pnlTxtContainer.SuspendLayout();
            this.pnlTxtWrapper.SuspendLayout();
            this.pnlLeftActions.SuspendLayout();
            this.SuspendLayout();

            // splitContainer
            this.splitContainer.BackColor = System.Drawing.Color.FromArgb(30, 20, 10); // Đường kẻ giữa rất tối
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // Panel 1 (Danh sách)
            this.splitContainer.Panel1.BackColor = clrBookshelf;
            this.splitContainer.Panel1.Controls.Add(this.flChatList);
            this.splitContainer.Panel1.Controls.Add(this.pnlSearch);
            this.splitContainer.Panel1.Controls.Add(this.pnlLeftHeader);
            // Panel 2 (Chat)
            this.splitContainer.Panel2.BackColor = clrRoom; // Màu giả lập căn phòng cũ
            // GỢI Ý: Bạn hãy set BackgroundImage cho Panel2 là hình căn phòng
            this.splitContainer.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer.Panel2.Controls.Add(this.flMessages);
            this.splitContainer.Panel2.Controls.Add(this.pnlInputArea);
            this.splitContainer.Panel2.Controls.Add(this.pnlRightHeader);
            this.splitContainer.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 0;

            // Header Left (Gỗ tối)
            this.pnlLeftHeader.BackColor = clrWoodDark;
            this.pnlLeftHeader.Controls.Add(this.btnAddFriend);
            this.pnlLeftHeader.Controls.Add(this.btnCreateGroup);
            this.pnlLeftHeader.Controls.Add(this.btnAccept);
            this.pnlLeftHeader.Controls.Add(this.lblTitle);
            this.pnlLeftHeader.Controls.Add(this.btnMenu);
            this.pnlLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLeftHeader.Height = 60;
            this.pnlLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftHeader.Name = "pnlLeftHeader";
            this.pnlLeftHeader.Size = new System.Drawing.Size(300, 60);
            this.pnlLeftHeader.TabIndex = 0;

            // BtnMenu
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnMenu.ForeColor = clrPaper; // Màu chữ vàng nhạt
            this.btnMenu.Location = new System.Drawing.Point(5, 10);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(40, 40);
            this.btnMenu.TabIndex = 3;
            this.btnMenu.Text = "☰";
            this.btnMenu.UseVisualStyleBackColor = false;

            // Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = clrPaper;
            this.lblTitle.Location = new System.Drawing.Point(50, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(104, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Nhắn tin";

            // Buttons Left Header (Style Đồng/Gỗ)
            this.btnAddFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFriend.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFriend.BackgroundColor = clrWoodLight; // Màu nút gỗ sáng
            this.btnAddFriend.BorderRadius = 10; // Bo góc nhẹ như nút gỗ
            this.btnAddFriend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddFriend.FlatAppearance.BorderSize = 0;
            this.btnAddFriend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFriend.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddFriend.ForeColor = clrTextLight;
            this.btnAddFriend.Location = new System.Drawing.Point(250, 10);
            this.btnAddFriend.Name = "btnAddFriend";
            this.btnAddFriend.Size = new System.Drawing.Size(40, 40);
            this.btnAddFriend.TabIndex = 1;
            this.btnAddFriend.Text = "👤+";
            this.btnAddFriend.UseVisualStyleBackColor = false;

            this.btnCreateGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateGroup.BackgroundColor = clrWoodLight;
            this.btnCreateGroup.BorderRadius = 10;
            this.btnCreateGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateGroup.FlatAppearance.BorderSize = 0;
            this.btnCreateGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateGroup.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreateGroup.ForeColor = clrTextLight;
            this.btnCreateGroup.Location = new System.Drawing.Point(205, 10);
            this.btnCreateGroup.Name = "btnCreateGroup";
            this.btnCreateGroup.Size = new System.Drawing.Size(40, 40);
            this.btnCreateGroup.TabIndex = 4;
            this.btnCreateGroup.Text = "👥";
            this.btnCreateGroup.UseVisualStyleBackColor = false;

            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.BackColor = System.Drawing.Color.Transparent;
            this.btnAccept.BackgroundColor = clrWoodLight;
            this.btnAccept.BorderRadius = 10;
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccept.ForeColor = clrTextLight;
            this.btnAccept.Location = new System.Drawing.Point(160, 10);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(40, 40);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "🔔";
            this.btnAccept.UseVisualStyleBackColor = false;

            // Search Panel
            this.pnlSearch.BackColor = clrBookshelf; // Nền cùng màu bookshelf
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Height = 50;
            this.pnlSearch.Location = new System.Drawing.Point(0, 60);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.pnlSearch.TabIndex = 1;

            // Search Box (Style tối, chữ sáng)
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(60, 45, 35); // Nâu đen trong suốt
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.ForeColor = System.Drawing.Color.NavajoWhite;
            this.txtSearch.Location = new System.Drawing.Point(15, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(270, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Tìm kiếm...";

            // Chat List
            this.flChatList.AutoScroll = true;
            this.flChatList.BackColor = clrBookshelf; // Màu kệ sách
            this.flChatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flChatList.Location = new System.Drawing.Point(0, 110);
            this.flChatList.Name = "flChatList";
            this.flChatList.Padding = new System.Windows.Forms.Padding(5);
            this.flChatList.Size = new System.Drawing.Size(300, 490);
            this.flChatList.TabIndex = 2;

            // Header Right (Gỗ tối)
            this.pnlRightHeader.BackColor = clrWoodDark;
            this.pnlRightHeader.Controls.Add(this.btnMoreInfo);
            this.pnlRightHeader.Controls.Add(this.btnVideoCall);
            this.pnlRightHeader.Controls.Add(this.btnVoiceCall);
            this.pnlRightHeader.Controls.Add(this.lblChatStatus);
            this.pnlRightHeader.Controls.Add(this.lblChatName);
            this.pnlRightHeader.Controls.Add(this.picChatAvatar);
            this.pnlRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRightHeader.Height = 65;
            this.pnlRightHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlRightHeader.Name = "pnlRightHeader";
            this.pnlRightHeader.Size = new System.Drawing.Size(699, 65);
            this.pnlRightHeader.TabIndex = 0;

            this.picChatAvatar.Location = new System.Drawing.Point(15, 10);
            this.picChatAvatar.Name = "picChatAvatar";
            this.picChatAvatar.Size = new System.Drawing.Size(45, 45);
            this.picChatAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picChatAvatar.TabStop = false;

            this.lblChatName.AutoSize = true;
            this.lblChatName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChatName.ForeColor = clrPaper; // Tên màu giấy cũ
            this.lblChatName.Location = new System.Drawing.Point(70, 8);
            this.lblChatName.Name = "lblChatName";
            this.lblChatName.Size = new System.Drawing.Size(122, 25);
            this.lblChatName.TabIndex = 1;
            this.lblChatName.Text = "Người dùng";

            this.lblChatStatus.AutoSize = true;
            this.lblChatStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChatStatus.ForeColor = System.Drawing.Color.DarkGray;
            this.lblChatStatus.Location = new System.Drawing.Point(72, 36);
            this.lblChatStatus.Name = "lblChatStatus";
            this.lblChatStatus.Size = new System.Drawing.Size(92, 15);
            this.lblChatStatus.TabIndex = 2;
            this.lblChatStatus.Text = "Đang hoạt động";

            // Buttons Right Header (Màu vàng đồng)
            this.btnMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoreInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoreInfo.FlatAppearance.BorderSize = 0;
            this.btnMoreInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoreInfo.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.btnMoreInfo.ForeColor = System.Drawing.Color.IndianRed; // Đỏ gạch
            this.btnMoreInfo.Location = new System.Drawing.Point(647, 12);
            this.btnMoreInfo.Name = "btnMoreInfo";
            this.btnMoreInfo.Size = new System.Drawing.Size(40, 40);
            this.btnMoreInfo.TabIndex = 3;
            this.btnMoreInfo.Text = "🗑️";
            this.btnMoreInfo.UseVisualStyleBackColor = true;

            this.btnVideoCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVideoCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVideoCall.FlatAppearance.BorderSize = 0;
            this.btnVideoCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVideoCall.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.btnVideoCall.ForeColor = System.Drawing.Color.BurlyWood; // Màu gỗ sáng
            this.btnVideoCall.Location = new System.Drawing.Point(555, 12);
            this.btnVideoCall.Name = "btnVideoCall";
            this.btnVideoCall.Size = new System.Drawing.Size(40, 40);
            this.btnVideoCall.TabIndex = 7;
            this.btnVideoCall.Text = "🎥";
            this.btnVideoCall.UseVisualStyleBackColor = true;

            this.btnVoiceCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoiceCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoiceCall.FlatAppearance.BorderSize = 0;
            this.btnVoiceCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoiceCall.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.btnVoiceCall.ForeColor = System.Drawing.Color.BurlyWood;
            this.btnVoiceCall.Location = new System.Drawing.Point(601, 12);
            this.btnVoiceCall.Name = "btnVoiceCall";
            this.btnVoiceCall.Size = new System.Drawing.Size(40, 40);
            this.btnVoiceCall.TabIndex = 6;
            this.btnVoiceCall.Text = "📞";
            this.btnVoiceCall.UseVisualStyleBackColor = true;

            // flMessages
            this.flMessages.AutoScroll = true;
            this.flMessages.BackColor = System.Drawing.Color.Transparent; // Để hiện nền phòng
            this.flMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flMessages.Location = new System.Drawing.Point(0, 65);
            this.flMessages.Name = "flMessages";
            this.flMessages.Padding = new System.Windows.Forms.Padding(10);
            this.flMessages.Size = new System.Drawing.Size(699, 475);
            this.flMessages.TabIndex = 2;

            // Input Area (Gỗ tối)
            this.pnlInputArea.BackColor = clrWoodDark;
            this.pnlInputArea.Controls.Add(this.pnlTxtContainer);
            this.pnlInputArea.Controls.Add(this.pnlLeftActions);
            this.pnlInputArea.Controls.Add(this.btnSend);
            this.pnlInputArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInputArea.Height = 60;
            this.pnlInputArea.Location = new System.Drawing.Point(0, 540);
            this.pnlInputArea.Name = "pnlInputArea";
            this.pnlInputArea.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlInputArea.TabIndex = 1;

            this.pnlTxtContainer.Controls.Add(this.pnlTxtWrapper);
            this.pnlTxtContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTxtContainer.Location = new System.Drawing.Point(110, 5);
            this.pnlTxtContainer.Name = "pnlTxtContainer";
            this.pnlTxtContainer.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTxtContainer.Size = new System.Drawing.Size(529, 50);
            this.pnlTxtContainer.TabIndex = 2;

            // Wrapper khung chat (Giấy cũ)
            this.pnlTxtWrapper.BackColor = clrPaper;
            this.pnlTxtWrapper.Controls.Add(this.txtMessage);
            this.pnlTxtWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTxtWrapper.Location = new System.Drawing.Point(5, 5);
            this.pnlTxtWrapper.Name = "pnlTxtWrapper";
            this.pnlTxtWrapper.Size = new System.Drawing.Size(519, 40);
            this.pnlTxtWrapper.TabIndex = 0;
            this.pnlTxtWrapper.Paint += (s, e) => {
                System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath();
                int rad = 10; // Bo góc ít hơn cho cổ điển
                p.AddArc(0, 0, rad, rad, 180, 90);
                p.AddArc(pnlTxtWrapper.Width - rad, 0, rad, rad, 270, 90);
                p.AddArc(pnlTxtWrapper.Width - rad, pnlTxtWrapper.Height - rad, rad, rad, 0, 90);
                p.AddArc(0, pnlTxtWrapper.Height - rad, rad, rad, 90, 90);
                p.CloseFigure();
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(pnlTxtWrapper.BackColor)) e.Graphics.FillPath(b, p);
            };
            this.pnlTxtWrapper.Resize += (s, e) => this.pnlTxtWrapper.Invalidate();

            // Text Message (Màu giấy cũ, chữ đen)
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.BackColor = clrPaper;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMessage.ForeColor = System.Drawing.Color.FromArgb(40, 20, 10);
            this.txtMessage.Location = new System.Drawing.Point(10, 10);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(494, 20);
            this.txtMessage.TabIndex = 0;

            // BtnSend (Style cây bút lông gà)
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI Symbol", 20F); // Icon to hơn
            this.btnSend.ForeColor = System.Drawing.Color.BurlyWood; // Màu gỗ sáng
            this.btnSend.Location = new System.Drawing.Point(639, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(60, 50);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "✒️"; // Thay bằng biểu tượng bút lông
            this.btnSend.UseVisualStyleBackColor = true;

            this.pnlLeftActions.Controls.Add(this.btnEmoji);
            this.pnlLeftActions.Controls.Add(this.btnAttach);
            this.pnlLeftActions.Controls.Add(this.btnImage);
            this.pnlLeftActions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftActions.Location = new System.Drawing.Point(0, 5);
            this.pnlLeftActions.Name = "pnlLeftActions";
            this.pnlLeftActions.Size = new System.Drawing.Size(110, 50);
            this.pnlLeftActions.TabIndex = 5;

            // Các nút chức năng dưới (Màu nâu đồng)
            Color clrBtnAction = System.Drawing.Color.FromArgb(160, 120, 90);

            this.btnEmoji.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmoji.FlatAppearance.BorderSize = 0;
            this.btnEmoji.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmoji.Font = new System.Drawing.Font("Segoe UI Emoji", 12F);
            this.btnEmoji.ForeColor = clrBtnAction;
            this.btnEmoji.Location = new System.Drawing.Point(75, 10);
            this.btnEmoji.Name = "btnEmoji";
            this.btnEmoji.Size = new System.Drawing.Size(30, 30);
            this.btnEmoji.TabIndex = 2;
            this.btnEmoji.Text = "🙂"; // Icon đơn giản
            this.btnEmoji.UseVisualStyleBackColor = true;

            this.btnAttach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttach.FlatAppearance.BorderSize = 0;
            this.btnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttach.Font = new System.Drawing.Font("Segoe UI Emoji", 12F);
            this.btnAttach.ForeColor = clrBtnAction;
            this.btnAttach.Location = new System.Drawing.Point(40, 10);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(30, 30);
            this.btnAttach.TabIndex = 1;
            this.btnAttach.Text = "📎";
            this.btnAttach.UseVisualStyleBackColor = true;

            this.btnImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImage.FlatAppearance.BorderSize = 0;
            this.btnImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImage.Font = new System.Drawing.Font("Segoe UI Emoji", 12F);
            this.btnImage.ForeColor = clrBtnAction;
            this.btnImage.Location = new System.Drawing.Point(5, 10);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(30, 30);
            this.btnImage.TabIndex = 0;
            this.btnImage.Text = "🖼";
            this.btnImage.UseVisualStyleBackColor = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "UC_Message";
            this.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlLeftHeader.ResumeLayout(false);
            this.pnlLeftHeader.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlRightHeader.ResumeLayout(false);
            this.pnlRightHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChatAvatar)).EndInit();
            this.pnlInputArea.ResumeLayout(false);
            this.pnlTxtContainer.ResumeLayout(false);
            this.pnlTxtWrapper.ResumeLayout(false);
            this.pnlTxtWrapper.PerformLayout();
            this.pnlLeftActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}