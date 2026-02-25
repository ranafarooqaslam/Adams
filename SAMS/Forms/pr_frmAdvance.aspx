<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmAdvance.aspx.cs" Inherits="pr_frmAdvance" Title="Advance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Forms/UserControl/MultiCheckCombo.ascx" tagname="MultiCheckCombo" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        .customerSerch input
        {
            -moz-box-shadow: inset 0 0 6px #333;
            -webkit-box-shadow: inset 0 0 6px #333;
            box-shadow: inset 0 0 6px #333;
            -moz-border-radius: 3px;
            height: 26px;
            float: left;
            padding-left: 5px;
            border: none;
            color: #666;
            margin-top: 10px;
            margin-left: 10px;
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
</style>    
<script language="JavaScript" type="text/javascript">

    function pageLoad() {

        //find the current popup
        var popUp = $find('ModelPopup');

        //check it exists so the script won't fail
        if (popUp) {
            //Add the function below as the event
            popUp.add_hidden(HidePopupPanel);
        }
    }

    function HidePopupPanel(source, args) {
        //find the panel associated with the extender
        objPanel = document.getElementById(source._PopupControlID);

        //check the panel exists
        if (objPanel) {
            //set the display attribute, so it remains hidden on postback
            objPanel.style.display = 'none';
        }
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
    prm.add_beginRequest(BeginRequestHandler);
    // Raised after an asynchronous postback is finished and control has been returned to the browser.
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.show();
        }
    }

    function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }

    </script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="right_data">
            <div>
                            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="31px" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </div>
                <asp:Panel ID="pnlAdvanceContent" runat="server">
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label3" runat="server" Text="Location"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"  runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label5" runat="server"  Text="Employee"></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <asp:DropDownList ID="ddlEmployee"   runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table> 
                <hr />
                <table width="100%">
                    <tr>
                        <td width="5px">
                            <asp:HiddenField ID="hdnAdvanceID" runat="server" />
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                <tr>
                                <td height="20px">
                                </td>
                                </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label2" runat="server"  Text="Amount"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <cc1:FilteredTextBoxExtender ID="txtAmount_FilteredTextBoxExtender" runat="server" TargetControlID="txtAmount"  ValidChars=".0123456789"></cc1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="10%">
                                            
                                        </td>
                                        <td width="50%">
                                        
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Approved By"></asp:Label>
                                        </td>
                                        <td>
                                             <uc1:MultiCheckCombo ID="mcbManagers" runat="server" />
                                        </td>
                                        <td>
                                        
                                            <asp:Label ID="Label8" runat="server"  Text="ApproveDate"></asp:Label>
                                        </td>
                                        <td>
                                         <cc1:CalendarExtender ID="txtApproveDate_CalendarExtender" runat="server" TargetControlID="txtApproveDate" PopupButtonID="txtApproveDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                           <asp:TextBox ID="txtApproveDate" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>  
                <table>
                </table>                            
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
                                <asp:Image ID="imgAdvance" runat="server" ImageUrl="../images/btn-save.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <asp:Panel ID="pnlAdvanceGrid" runat="server">
                    <asp:LinkButton ID="btnShowpnAdvanceContent" OnClick="btnShowpnAdvanceContent_Click" runat="server">Add New Advance</asp:LinkButton>
                <asp:Repeater ID="rptAdvance" runat="server" OnItemCommand="rptAdvance_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="25%">
                                        Employee
                                    </td>
                                    <td width="15%">
                                         Type
                                    </td>
                                    <td width="15%">
                                        Amount
                                    </td>
                                    <td width="15%">
                                       No of Installments
                                    </td>
                                     <td width="20%">
                                       Approved By
                                    </td>                                  
                                    <td width="10%" colspan="2">
                                        Action
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                             <%# Eval("EmployeeFullName")%>                             
                            </td>
                            <td>
                             Advance
                            </td>
                             <td>
                             <%# Eval("Amount")%>
                            </td>
                            <td>
                                 <%# Eval("NoOfInstallments")%>                                
                            </td>
                             <td>
                                 <%# Eval("ApprovedBy")%>                                  
                            </td>                               
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("Loan_LeaseID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("Loan_LeaseID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                       
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <tr style="background-color:#E4E4E4">
                            <td>
                             <%# Eval("EmployeeFullName")%>                             
                            </td>
                            <td>
                             Advance
                            </td>
                             <td>
                             <%# Eval("Amount")%>
                            </td>
                            <td>
                                 <%# Eval("NoOfInstallments")%>                                
                            </td>
                             <td>
                                 <%# Eval("ApprovedBy")%>                                  
                            </td>                              
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("Loan_LeaseID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>                  
                             <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("Loan_LeaseID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                       
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
