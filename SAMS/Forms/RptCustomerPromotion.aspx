<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptCustomerPromotion.aspx.cs" Inherits="Forms_RptCustomerPromotion" Title="SAMS :: Customer Promotion Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("select").searchable();
        }
    </script>
    <script type="text/javascript">
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

        function showPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
        function hidepopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
        }
    </script>    
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
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%" cellpadding="2">
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" Text="Location"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="Channel Type"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlChannel" runat="server" Width="200px" 
                                            AutoPostBack="True" onselectedindexchanged="ddlChannel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="Customer"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="In Active"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label4" runat="server" Text="Date"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:TextBox ID="txtDate" runat="server" onkeyup="BlockEndDateKeyPress()" Width="150px"></asp:TextBox>
                                        <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                        <cc1:CalendarExtender ID="CESDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibDate"
                                            TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                            </table>                            
                            <table width="100%" cellpadding="2">
                                <tr>
                                    <td colspan="4">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%">                                        
                                    </td>
                                    <td style="width:25%">
                                        <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                                            Width="90" OnClick="btnViewExcel_Click" />
                                    </td>
                                    <td style="width:10%">                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                            <Triggers>                                
                             <asp:PostBackTrigger ControlID="btnViewExcel"/>
                            </Triggers>
                    </asp:UpdatePanel>                                        
                </td>
            </tr>
        </table>
    </div>
</asp:Content>