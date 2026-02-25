using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web.UI.WebControls;
using System.Linq;

public partial class UserControl_MultiCheckCombo1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCombo.Attributes.Add("readonly", "readyonly");
    }
    public int WidthCheckListBox
    {
        set
        {
            chkList.Width = value;
        }
    }
    /// <summary>
    /// Set the Width of the Combo
    /// </summary>
    public int Width
    {
        set { txtCombo.Width = value; }
        get { return (Int32)txtCombo.Width.Value; }
    }
    public bool Enabled
    {
        set { txtCombo.Enabled = value; }
    }
    /// <summary>
    /// Set the CheckBoxList font Size
    /// </summary>
    public FontUnit fontSizeCheckBoxList
    {
        set { chkList.Font.Size = value; }
        get { return chkList.Font.Size; }
    }
    /// <summary>
    /// Set the ComboBox font Size
    /// </summary>
    public FontUnit fontSizeTextBox
    {
        set { txtCombo.Font.Size = value; }
    }
    /// <summary>
    /// Add Items to the CheckBoxList.
    /// </summary>
    /// <param name="array">ArrayList to be added to the CheckBoxList</param>
    public List<ListItem> getSelectedItems()
    {
        List<ListItem> selected = chkList.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
        return selected;
    }
    public void AddItems(DataTable dt, string value, string text)
    {
        foreach (DataRow dr in dt.Rows)
        {
            ListItem  li=new ListItem ();
            li.Value=dr[value].ToString();
            li.Text=dr[text].ToString();
            chkList.Items.Add(li);
        }
    }
    public void AddItems(DataTable dt, int value, int text)
    {
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr[value].ToString();
            li.Text = dr[text].ToString();
            chkList.Items.Add(li);
        }
    }
    public void AddItems(DataTable dt, string value, int text)
    {
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr[value].ToString();
            li.Text = dr[text].ToString();
            chkList.Items.Add(li);
        }
    }
    public void AddItems(DataTable dt, int value, string text)
    {
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr[value].ToString();
            li.Text = dr[text].ToString();
            chkList.Items.Add(li);
        }
    }
    /// <summary>
    /// Add Items to the CheckBoxList
    /// </summary>
    /// <param name="dr"></param>
    /// <param name="nombreCampoTexto">Field Name of the OdbcDataReader to Show in the CheckBoxList</param>
    /// <param name="nombreCampoValor">Value Field of the OdbcDataReader to be added to each Field Name (it can be the same string of the textField)</param>
    public void AddItems(OdbcDataReader dr, string textField, string valueField)
    {
        ClearAll();
        int i = 0;
        while (dr.Read())
        {
            chkList.Items.Add(dr[textField].ToString());
            chkList.Items[i].Value = i.ToString();
            i++;
        }
    }
    /// <summary>
    /// Uncheck of the Items of the CheckBox
    /// </summary>
    public void unselectAllItems()
    {
        for (int i = 0; i < chkList.Items.Count; i++)
        {
            chkList.Items[i].Selected = false;
        }
    }
    /// <summary>
    /// Delete all the Items of the CheckBox;
    /// </summary>
    public void ClearAll()
    {
        txtCombo.Text = "";
        chkList.Items.Clear();
    }
    /// <summary>
    /// Get or Set the Text shown in the Combo
    /// </summary>
    public string Text
    {
        get { return hidVal.Value; }
        set { txtCombo.Text = value; }
    }
    public void selectItem(string text)
    {
        foreach (ListItem li in chkList.Items)
        {
            if (li.Text == text)
            {
                li.Selected = true;
            }
        }        
    }
}