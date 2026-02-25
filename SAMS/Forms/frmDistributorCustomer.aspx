<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDistributorCustomer.aspx.cs" Inherits="Forms_frmDistributorCustomer" Title="SAMS :: New Customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

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

        function NameValidation(event) {
            // Allow: backspace, delete, tab and escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash, Underscoor
            (event.keyCode == 189) ||
            //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
            //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function AddressValidation(event) {
            // Allow: backspace, delete, tab , escape and space bar
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash, Underscoor
            (event.keyCode == 189) ||
            // Allow: Open bracket, Close bracket
            ((event.keyCode == 57 || event.keyCode == 48) && event.shiftKey === true) ||
            //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            (event.keyCode >= 96 && event.keyCode <= 105) || //Keypad numbers
            //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function PhoneValidation(event) {
            // Allow: backspace, delete, tab , escape
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash
            (event.keyCode == 189 && event.shiftKey === false) ||
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            //Keypad numbers
            (event.keyCode >= 96 && event.keyCode <= 105)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                event.preventDefault();
            }
        }

        function pageLoad() {
                $("select").searchable();
            $('#<%=txtCNIC.ClientID %>').mask("99999-9999999-9");
            //$('#<%=txtNTN.ClientID %>').mask("^[a-zA-Z0-9_ ]*$");
            
            $('#<%=Grid_users.ClientID %>').tablesorter(
	     {
	         headers: {
	             25: {
	                 sorter: false
	             }
	         }
	     }
	     );

        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
        }


        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtCustomerName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Customer Name');
                return false;
            }
            str = document.getElementById('<%=txtContactPerson.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Contact Person Name');
                return false;
            }
            str = document.getElementById('<%=txtAddress.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Address');
                return false;
            }

            str = document.getElementById('<%=txtPhoneNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Phone No');
                return false;
            }
            str = document.getElementById('<%=txtRegdate.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Register Date');
                return false;
            }

            str = document.getElementById('<%=txtCNIC.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter CNIC #');
                return false;
            }

            str = document.getElementById('<%=txtNTN.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter NTN #');
                return false;
            }

            if ($('#<%= ChbIsRegister.ClientID %>').is(':checked')) {
                str = document.getElementById('<%=txtIsRegister.ClientID%>').value;
                if (str == null || str.length == 0) {
                    alert('Must enter STRN #');
                    return false;
                }
            }

            return true;
        }
        function SearchRecord() {
            var str;
            str = document.getElementById('<%=txtSeach.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Key Word for Searching');
                return false;
            }
            return true;
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
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Text="Location"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" 
                                                OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Town"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:DropDownList ID="DrpTown" runat="server" Width="205px" OnSelectedIndexChanged="DrpTown_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Text="Route"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpRoute" runat="server" Width="205px" 
                                                OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Text="Market"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:DropDownList ID="DrpMarket" runat="server" Width="206px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lblNickName" runat="server" Text="Channel Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="drpChannelType" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lblMobileNo" runat="server" Text="Business Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:DropDownList ID="DrpBusinessType" runat="server" Width="206px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtCustomerName" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Text="Contact Person"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:TextBox ID="txtContactPerson" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lblPhNo" runat="server" Text="Phone No"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px" MaxLength="15"></asp:TextBox>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lblAddress1" runat="server" Text="Address "></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:TextBox ID="txtAddress" runat="server" Width="200px" MaxLength="250"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label8" runat="server" Text="CNIC #"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtCNIC" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lbldesignationID" runat="server" Text="Register Date"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:TextBox ID="txtRegdate" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lblNTN" runat="server" Text="NTN #"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtNTN" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td style="width:10%">
                                            <asp:CheckBox ID="ChbIsRegister" runat="server" Width="96px" Text="Is Register" AutoPostBack="True"
                                                    OnCheckedChanged="ChbIsRegister_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td style="width:50%">
                                            <asp:TextBox ID="txtIsRegister" runat="server" Width="200px" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label15" runat="server" Text="Price Group"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="drpPriceGroup" runat="server" Width="205"></asp:DropDownList>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="lblPromotion" runat="server" Text="Promotion Class"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:DropDownList ID="ddlPromotionClass" runat="server" Width="205"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Latitude"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtLat" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Text="Longitude"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtLong" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Tax Slab"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpTaxSlab" runat="server" Width="205" AutoPostBack="true" OnSelectedIndexChanged="DrpTaxSlab_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                         <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label12" runat="server" Text="Tax Clause"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:DropDownList ID="DrpSlabType" runat="server" Width="205"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" align="left">
                                            </td>
                                            <td style="width: 175px">
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Width="80px"
                                                    Font-Size="8pt" Text="Cancel" CssClass="Button" />
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True">
                                                </asp:CheckBox>
                                            </td>
                                            <td style="width: 219px">
                                            </td>



                                             
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 244px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                        width: 100%; border-bottom: silver thin inset; background-color: silver">
                        <tbody>
                            <tr>
                                <td style="height: 21px" align="left">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
                                        <asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
                                        <asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
                                        <asp:ListItem Value="ADDRESS">Address</asp:ListItem>
                                        <asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
                                        <asp:ListItem Value="GEO_NAME">Town Name</asp:ListItem>
                                        <asp:ListItem Value="AREA_NAME">Route Name</asp:ListItem>
                                        <asp:ListItem Value="ROUTE_NAME">Market Name</asp:ListItem>
                                        <asp:ListItem Value="SLASH_DESC">Channel Type</asp:ListItem>
                                        <asp:ListItem>CNIC</asp:ListItem>
                                        <asp:ListItem>NTN</asp:ListItem>
                                        <asp:ListItem Value="GST_NUMBER">STRN</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="height: 21px" align="left" width="250">
                                    <asp:Button ID="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnSearch_Click"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White"
                            OnRowCommand="Grid_users_RowCommand">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                            <RowStyle ForeColor="Black"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BUSINESS_TYPE_ID" HeaderText="BUSINESS_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROMOTION_CLASS" HeaderText="PROMOTION_CLASS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TOWN_ID" HeaderText="TOWN_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_ID" HeaderText="AREA_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_ID" HeaderText="ROUTE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST_NUMBER" HeaderText="Gst No" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ChannelType" HeaderText="Channel Type" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GEO_NAME" HeaderText="Town" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AREA_NAME" HeaderText="Route" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROUTE_NAME" HeaderText="Market" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status" HtmlEncode="false">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REGDATE" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_STAND" HeaderText="Stand" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_COOLER" HeaderText="Cooler" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CNIC" HeaderText="CNIC" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NTN" HeaderText="NTN" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GST" HeaderText="GST" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="Latitude" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="Longitude" HtmlEncode="false">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsShifted">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="TAX_SLAB_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="CLAUSE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        Width="35px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White"></FooterStyle>
                            <PagerStyle BackColor="Transparent"></PagerStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
                                Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333">
                            </AlternatingRowStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
