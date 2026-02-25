<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptSaleDataCollection.aspx.cs" Inherits="Forms_RptSaleDataCollection" Title="SAMS :: Retail Sale Data Collection" %>
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
                                            <asp:Label ID="Label8" runat="server" Text="Type"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:RadioButtonList ID="rbReportType" runat="server" RepeatDirection="Horizontal" >
                                            <asp:ListItem Value="0" Text="Sale" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Sale Return"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                  <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" Text="Location"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="lblfromLocation" runat="server" Text="Customer Route"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label7" runat="server" Text="Customer Type"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlCustomerType" runat="server" Width="200px">
                                            <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Registered"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Non-Registered"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px">
                                            </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" Text="Principal"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>                                
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="lblOrderbooker" runat="server" Text="Orderbooker"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="DrpOrderbooker" runat="server" Width="199px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Text="Sale Force"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="DrpSaleForce" runat="server" Width="199px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td style="width:50%">
                                    <table width="100%" cellpadding="2">
                                        <tr>
                                            <td style="width:30%">
                                                <strong>
                                                    <asp:Label ID="lblNickName" runat="server" Text="Channel Type"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width:70%">
                                                <asp:CheckBox ID="cbChannelAll" runat="server" Text="All Channel Types" AutoPostBack="true"
                                                    oncheckedchanged="cbChannelAll_CheckedChanged" />
                                                <asp:Panel ID="Panel1" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                        BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="cblChannel" runat="server">
                                                </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:30%">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Text="Category"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width:70%">
                                                <asp:CheckBox ID="cbCategoryAll" runat="server" Text="All Categories" AutoPostBack="true"
                                                    oncheckedchanged="cbCategoryAll_CheckedChanged" />
                                                <asp:Panel ID="Panel2" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                    BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="cblCategory" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="cblCategory_SelectedIndexChanged">
                                                </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>`
                                    </td>
                                    <td style="width:50%" valign="top">
                                        <table width="100%" cellpadding="2">
                                        <tr>
                                            <td style="width:15%">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Text="SKU"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width:85%">
                                                <asp:CheckBox ID="cbSKUAll" runat="server" Text="All SKUs" AutoPostBack="true"
                                                    oncheckedchanged="cbSKUAll_CheckedChanged" />
                                                <asp:Panel ID="Panel3" runat="server" Width="295px" Height="330px" ScrollBars="Vertical"
                                                    BorderStyle="Groove" BorderWidth="1px">
                                                <asp:CheckBoxList ID="cblSKU" runat="server">
                                                </asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="2">
                                <tr>
                                    <td style="width:15%">
                                        
                                    </td>
                                    <td style="width:25%">
                                        <strong>
                                            <asp:CheckBox ID="cbWithSKUName" runat="server" Text="With SKU Name" />
                                        </strong>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:TextBox ID="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server"
                                            Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                        </asp:ImageButton>
                                        <cc1:CalendarExtender ID="CEStartDate" runat="server" TargetControlID="txtStartDate"
                                                PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server"
                                            Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                        <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                        </asp:ImageButton>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width:10%">
                                        
                                    </td>
                                    <td style="width:50%"></td>
                                </tr>
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