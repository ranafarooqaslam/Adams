<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAccountHeadAssignment.aspx.cs" Inherits="Forms_frmAccountHeadAssignment"
    Title="Account Head Assignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">    

<script language="JavaScript" type="text/javascript">
    function pageLoad() {
        $("select").searchable();
    }

    function checkAll(CheckBox) {
        TotalChkBx = parseInt('<%= gvAccountHead.Rows.Count %>');
        var TargetBaseControl = document.getElementById('<%= gvAccountHead.ClientID %>');
        var TargetChildControl = "cbField";
        var Inputs = TargetBaseControl.getElementsByTagName("input");
        for (var iCount = 0; iCount < Inputs.length; ++iCount) {
            if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[iCount].checked = CheckBox.checked;
        }
    }

    function SelectDeSelectHeader(CheckBox) {
        TotalChkBx = parseInt('<%= gvAccountHead.Rows.Count %>');
        var TargetBaseControl = document.getElementById('<%= gvAccountHead.ClientID %>');
        var TargetChildControl = "cbField";
        var TargetHeaderControl = "cbFieldHeader";
        var Inputs = TargetBaseControl.getElementsByTagName("input");
        var flag = false;
        var HeaderCheckBox;
        for (var iCount = 0; iCount < Inputs.length; ++iCount) {
            if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) >= 0)
                HeaderCheckBox = Inputs[iCount];
            if (Inputs[iCount] != CheckBox && Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) == -1) {
                if (CheckBox.checked) {
                    if (!Inputs[iCount].checked) {
                        flag = false;
                        HeaderCheckBox.checked = false;
                        return;
                    }
                    else
                        flag = true;
                }
                else if (!CheckBox.checked)
                    HeaderCheckBox.checked = false;
            }
        }
        if (flag)
            HeaderCheckBox.checked = CheckBox.checked
    }

</script>
        
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="90%" align="center">
                                    <tbody>                                        
                                        <tr>
                                            
                                            <td style="width: 15%; height: 22px" align="left">
                                                <strong>Form:</strong>
                                            </td>
                                            
                                            <td style="width: 70%; height: 22px" align="left">
                                                <asp:DropDownList ID="ddlForm" runat="server" Width="200px" OnSelectedIndexChanged="ddlForm_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="0" Text="Sales Invoice"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Purchase Invoice"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Expenses"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 5%; height: 22px">
                                            </td>
                                        </tr>
                                        <tr>
                                          
                                            <td style="width: 15%; height: 22px" align="left">
                                                <strong>Control Account:</strong>
                                            </td>
                                           
                                            <td style="width: 70%; height: 22px" align="left">
                                                <asp:DropDownList ID="ddlAccountHead" runat="server" Width="200px">                                                    
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 5%; height: 22px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">         
                                            &nbsp;                                   
                                            </td>
                                        </tr>
                                        <tr>
                                                                                    
                                            <td style="width: 100%;" align="left" colspan="3">
                                                <asp:GridView ID="gvAccountHead" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" 
                                                    Width="80%" onrowdatabound="gvAccountHead_RowDataBound">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>                
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbField" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" Width="5%" Height="30px"/>
                                                                <HeaderStyle Width="5%" />
                                                                <HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <asp:CheckBox ID="cbFieldHeader" runat="server" onclick = "checkAll(this);" />
                                                                </ItemTemplate>    
                                                                </HeaderTemplate>
                                                            </asp:TemplateField>                                                                                                        
                                                            <asp:BoundField DataField="Field" HeaderText="Field">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" Width="35%"/>
                                                                <HeaderStyle Width="35%" HorizontalAlign="Left" />
                                                            </asp:BoundField>                                                            
                                                            <asp:TemplateField HeaderText="Account Head">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlAccountHead" runat="server" Width="100%"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="60%"/>
                                                                <HeaderStyle Width="60%" HorizontalAlign="Left" />
                                                            </asp:TemplateField>                                                            
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="Silver" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                            </td>
                                            <td style="width: 5%; height: 22px">
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td style="width: 100%" colspan="5">
                                                <div style="z-index: 101; left: 495px; width: 100px; position: absolute; top: 470px;
                                                    height: 100px">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                                            <ProgressTemplate>
                                                                &nbsp;<asp:ImageButton ID="btnImage" runat="server" Height="33px" Width="31px" ImageUrl="~/App_Themes/Granite/Images/image003.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                           
                                            <td >
                                                <asp:Button ID="btnAssign" runat="server" Text="Assign"
                                                    CssClass="Button" onclick="btnAssign_Click"  Width="80px"/>
                                                
                                            </td>
                                            <td style="width: 5%">
                                            </td>
                                        </tr>                                        
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
