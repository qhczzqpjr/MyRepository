using System;
using System.Linq;
using System.Windows.Forms;
using Business.Mapper;
using Business.Models;
using Business.Services;

namespace Portal
{
    public partial class FrmRequirement : Form
    {
        private readonly DbManagementService _dbManagementService = new DbManagementService();

        public FrmRequirement()
        {
            InitializeComponent();
        }
        Win32Api.KeyboardHook _kh;
        private void frmRequirement_Load(object sender, EventArgs e)
        {
            ObjectMapper.CreateMaps();
            _kh = new Win32Api.KeyboardHook();
            _kh.SetHook();
            _kh.OnKeyDownEvent += kh_OnKeyDownEvent;
            DataLoader.Instance.Init();
            _dbManagementService.GetAllRequirements().Select(p=>p.Code).ToList().ForEach(rd => cmbRequirementCode.Items.Add(rd));
            
            labMessage.Text = "";
        }

        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.S | Keys.Alt)) { Show(); }//Alt+S显示窗口
            if (e.KeyData == (Keys.H | Keys.Alt)) { Hide(); }//Alt+H隐藏窗口
            if (e.KeyData == (Keys.C | Keys.Alt)) { Close(); }//Alt+C 关闭窗口 
            //if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { Text = "你发现了什么？"; }//Ctrl+Alt+A
        }

        private void cmbRequirementDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            labMessage.Text = "";
            var strCode = cmbRequirementCode.Text;
            rtbContent.Text = _dbManagementService.GetRequirement(strCode).Context;
        }


        private void cmbRequirementDes_TextUpdate(object sender, EventArgs e)
        {
            var strCode = cmbRequirementCode.Text;
            rtbContent.Text = "";
            cmbRequirementCode.Items.Clear();
            _dbManagementService.GetRequirements(strCode)
                .Select(p => p.Code)
                .ToList()
                .ForEach(rd => cmbRequirementCode.Items.Add(rd));

            cmbRequirementCode.DroppedDown = true;
            cmbRequirementCode.Text = strCode;
            cmbRequirementCode.SelectionStart = cmbRequirementCode.Text.Length;
        }

        private void cmbRequirementDes_KeyDown(object sender, KeyEventArgs e)
        {
            if (rtbContent.Text != "")
            {
                Clipboard.SetDataObject(rtbContent.Text);
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Handle inputs strings
            var strNewContent = rtbContent.Text;
            var strCode = cmbRequirementCode.Text;

            var requirement = _dbManagementService.GetRequirement(strCode);
            if (requirement == null)
            {
                _dbManagementService.CreateRequirement(new Requirement {Code = strCode, Context = strNewContent});
            }
            else
            {
                if (requirement.Context == strNewContent)
                {
                    labMessage.Text = "No Changes";
                    return;
                }
                requirement.Context = strNewContent;
                _dbManagementService.UpdateRequirement(requirement);
            }
            labMessage.Text = "Save OK !!";
        }

        private void rtbContent_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            rtbContent.ContextMenuStrip = contextMenuStrip1;
            rtbContent.ContextMenuStrip.Show(rtbContent, e.Location);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var strCode = cmbRequirementCode.Text;

            var requirement = _dbManagementService.GetRequirement(strCode);
            if (requirement == null) return;
            _dbManagementService.DeleteRequirement(requirement);
            labMessage.Text = "Item Removed!!";
            cmbRequirementCode.Text = "";
            rtbContent.Text = "";
        }
    }
}