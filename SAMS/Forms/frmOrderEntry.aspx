<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOrderEntry.aspx.cs" Inherits="Forms_frmOrderEntry" Title="SAMS :: Order/Invoice Step 2" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function CustomerChanged()
        {
            var SelectedTex = $('#<%=ddlCustomer.ClientID %> option:selected').text();
            var CreditLimint = SelectedTex.substring(SelectedTex.indexOf("/") + 1);
            var CreditLimint2 = CreditLimint.substr(0, CreditLimint.indexOf('~'));
            var CreditUsed = SelectedTex.substring(SelectedTex.indexOf("~") + 1);
            var CreditUsed2 = CreditUsed.substr(0, CreditUsed.indexOf('='));
            var PriceGroup = SelectedTex.substring(SelectedTex.indexOf("=") + 1);
            document.getElementById("<%= txtPriceGroup.ClientID %>").value = PriceGroup;
            document.getElementById("<%= txtCreditLimit.ClientID %>").value = CreditLimint2;
            document.getElementById("<%= txtCreditUsed.ClientID %>").value = parseFloat(CreditUsed2) * -1;
            document.getElementById("<%= txtBalance.ClientID %>").value = parseFloat(CreditLimint2) + parseFloat(CreditUsed2);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;
        }
        function endRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
        }

        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }
        function ddlBlur(obj) {
            obj.className = "";
        }
        
        function pageLoad() {
            CustomerChanged();
            $("select").searchable();
            var Str = $('#<%=ddlSKuCde.ClientID %> option:selected').text();             
            var unitprice = Str.substring(Str.indexOf(':') + 1);           
            document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;            
            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var Str = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                var unitprice = Str.substring(Str.indexOf(':') + 1);                
                document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;                
            });

            $('#<%=txtQuantity.ClientID %>').keypress(function (event) {
                if (event.keyCode == 13) {
                    document.getElementById("<%= txtQuantity.ClientID %>").click();
                }
            });
        }

        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }

            return true;
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
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
    </div>
        <div>
            <span class="heading">Order/Invoice Step 2</span>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="74px" Text="Order No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="276px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblBillBook" runat="server" Text="Bill Book No:" CssClass="lblbox"
                                                        Visible="true"></asp:Label></strong>
                                                <asp:TextBox ID="txtBillBookNo" runat="server" CssClass="uppercase" MaxLength="18"
                                                   Width="125px" Visible="true"></asp:TextBox>
                                                <asp:DropDownList ID="DrpVehicleNo" runat="server" Visible="false" CssClass="DropList">
                                                </asp:DropDownList>
                                                <asp:CheckBox ID="ChbDiscount" runat="server" Visible="False" Width="90px" Text="Promotion"
                                                    AutoPostBack="True" Checked="True"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDC" runat="server" Width="74px" Text="DC/PO #"></asp:Label>
                                                </strong>
                                            </td>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:TextBox ID="txtDCPONo" runat="server" width="273px"></asp:TextBox>
                                            </td>
                                            <td valign="middle">
                                                <strong>
                                                    <asp:Label ID="lblPODate" runat="server" Width="74px" Text="PO Date:"></asp:Label></strong>
                                                <asp:TextBox ID="txtPODate" runat="server" Width="100px" Visible="true"></asp:TextBox>
                                                <asp:ImageButton ID="ibPODate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Height="13px" />
                                                <ajaxToolkit:CalendarExtender ID="cePODate" runat="server" EnableViewState="False"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ibPODate" TargetControlID="txtPODate">
                                                </ajaxToolkit:CalendarExtender>
                                             
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="left">
                                                <strong>
                                                    <asp:Label ID="Label11" runat="server" Width="77px" Text="Order Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:RadioButtonList ID="RblPayMode" runat="server" Width="250px" Height="1px" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                                                    <asp:ListItem Value="215">Credit</asp:ListItem>
                                                    <asp:ListItem Value="216">Advance</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td valign="middle" align="left" colspan="1">
                                                <strong>
                                                    <asp:Label ID="lblVehicle" runat="server" Text="Vehicle No :"></asp:Label></strong>&nbsp;
                                                <strong>
                                                    <asp:Label ID="lblVehicleNo" runat="server"></asp:Label></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="left">
                                                <strong>
                                                    <asp:Label ID="Label12" runat="server" Width="101px" Text="Route/Saleman" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td valign="top" align="left" colspan="2">
                                                <asp:TextBox ID="txtprincipal" onkeyup="SearchList()" runat="server" Width="277px"
                                                    CssClass="txtBox " ReadOnly="True" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="DrpRoute" runat="server" Width="276px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td valign="top" align="left" colspan="1">
                                                <asp:TextBox ID="txtDeliveryMan" onkeyup="SearchList()" runat="server" Width="200px"
                                                    CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 23px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="74px" Text="Customer"></asp:Label></strong>
                                            </td>
                                            <td style="height: 23px" align="left" colspan="2">
                                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="276" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChange" onchange="CustomerChanged();">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 23px" align="left">
                                                 <asp:TextBox ID="txtPriceGroup" runat="server" Width="105px"></asp:TextBox>
                                                 <asp:TextBox ID="txtDiscountType" onkeyup="SearchList()" runat="server" Width="90px"
                                                 ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width:15%;">
                                                            <strong>
                                                                <asp:Label ID="Label13" runat="server" Width="90px" Text="Credit Ceiling"></asp:Label>
                                                            </strong>                                                        
                                                        </td>
                                                        <td style="width:15%;">
                                                            <asp:TextBox ID="txtCreditLimit" runat="server" Width="76px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:15%;">
                                                            <strong>
                                                                <asp:Label ID="Label14" runat="server" Width="80px" Text="Credit Used"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width:15%;">
                                                            <asp:TextBox ID="txtCreditUsed" runat="server" Width="114px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:15%;">
                                                            <strong>
                                                                    <asp:Label ID="Label15" runat="server" Width="90px" Text="Credit Balance"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td style="width:15%;">
                                                            <asp:TextBox ID="txtBalance" runat="server" Width="107px"></asp:TextBox>
                                                        </td>
                                                        <td style="width:10%;">
                                                            <asp:CheckBox ID="ChbBatchNo" runat="server" Width="94px" Text="Batch No" AutoPostBack="True"
                                                                    OnCheckedChanged="ChbBatchNo_CheckedChanged" Visible="false"></asp:CheckBox>                                                        
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td rowspan="2" style="width: 321px;">
                        <asp:Panel ID="Panel7" runat="server" Width="100%" Height="321px" ScrollBars="None"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                            <asp:HiddenField ID="hfBillBookNo" runat="server" />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                        width: 100%; border-bottom: silver thin inset; background-color: silver">
                                        <tbody>
                                            <tr>
                                                <td style="height: 21px; width: 40%;" align="left">
                                                    <asp:Label ID="Label6" runat="server" Width="100%" Text="Customer Name"></asp:Label>
                                                </td>
                                                <td style="width: 40%; height: 21px" align="left">
                                                    <asp:TextBox ID="txtSeach" runat="server" Width="100%" CssClass="txtBox "></asp:TextBox>
                                                </td>
                                                <td style="height: 21px" align="left" width="20%;">
                                                    <asp:Button ID="btnSearch" runat="server" Width="100%" Font-Size="8pt" Text="Filter"
                                                        OnClick="btnSearch_Click"></asp:Button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:Panel ID="Panel6" runat="server" Width="100%" Height="290px" ScrollBars="Vertical">
                                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White">
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
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
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
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AREA_ID" HeaderText="AREA_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ROUTE_ID" HeaderText="ROUTE_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GST_NUMBER" HeaderText="Gst No">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ChannelType" HeaderText="Channel Type">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GEO_NAME" HeaderText="Town">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AREA_NAME" HeaderText="Route">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ROUTE_NAME" HeaderText="Market">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="REGDATE">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IS_STAND" HeaderText="Stand">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IS_COOLER" HeaderText="Cooler">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CNIC" HeaderText="CNIC">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NTN" HeaderText="NTN">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:CommandField>
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
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblskuname" runat="server" Width="250px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Description" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label3" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Quantity" Width="61px"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="lblquantity" runat="server" Width="62px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Unit Price" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 16px">
                                                <asp:Label ID="Label7" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Batch No" Width="100%" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label2" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Amount" Width="100%"></asp:Label>
                                            </td>
                                            <td style="height: 16px">
                                                <asp:Label ID="Label4" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Add SKU" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="249px" onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);" >
                                                    </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtBox " onfocus="SearchSKUCode();"
                                                    Width="55px"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUnitRate" runat="server" Width="56px" CssClass="txtBox" Enabled="false"></asp:TextBox>                                                
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBatchNo" runat="server" CssClass="txtBox " Enabled="False" Width="62px">N/A</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server"  Enabled="False" Width="72px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                                    ValidationGroup="vg" Width="100px" AccessKey="A" CssClass="Button" Height="28px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="7">
                                                <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="640px"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White"
                                                        ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" OnRowCommand="GrdPurchase_RowCommand">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="75px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="200px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" HeaderText="Batch No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                                    Width="80px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_CTN" HeaderText="QUANTITY_CTN">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GST_ON" HeaderText="GST_ON">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="STANDARD_DISCOUNT" HeaderText="STANDARD_DISCOUNT">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="EXTRA_DISCOUNT" HeaderText="STANDARD_DISCOUNT">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ADVANCE_TAX_PERCENT" HeaderText="ADVANCE_TAX_PERCENT">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ADVANCE_TAX" HeaderText="ADVANCE_TAX">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                                    Width="35px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                             <strong>Total Quantity: </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTotalQty" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>
                                           </td>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td>
                                             
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td valign="top" align="left" colspan="2" rowspan="7">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU" CssClass="lblbox"></asp:Label></strong><asp:Panel
                                                        ID="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                                        BorderWidth="1px">
                                                        <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                            BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                PreviousPageText="Previous"></PagerSettings>
                                                            <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black">
                                                            </RowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
                                                                    <ItemStyle Width="80px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                                    <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                                    <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                                                    </ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                            </td>
                                            <td style="height: 20px" align="left">
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo1" runat="server" Width="94px" Text="Gross Sale" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px">
                                                <asp:TextBox ID="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                     ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation1" runat="server" Width="94px" Text="Extra Discount"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px">
                                                <asp:TextBox ID="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True"  ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 18px" align="left">
                                            </td>
                                            <td style="height: 18px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label51" runat="server" Width="110px" Text="Standard Discount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 18px">
                                                <asp:TextBox ID="numTxtTotalStndrdDiscnt" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True"  ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                        --%><strong>
                                            <asp:Label ID="Label8" runat="server" Width="116px" Text="Claimable  Discount" CssClass="lblbox"
                                                Visible="false"></asp:Label></strong>
                                        <%--  </td>
                                            <td style="width: 7px; height: 20px" align="right">--%>
                                        <asp:TextBox ID="numtxtUnClaimabledist" runat="server" Width="150px" ForeColor="Black"
                                            Font-Bold="True"  ReadOnly="True" Visible="false"></asp:TextBox>
                                        <%--  </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label81" runat="server" Width="93px" Text="S. Tax Amount"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                     ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Text="Add. Tax Amount"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotalTST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                     ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top">
                                                <asp:HiddenField Visible="false" runat="server" ID="hfslabTaxPercent" Value="0" />
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label16" runat="server" Width="105px" Text="Advance Tax" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtAdvanceTax" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 20px" valign="top" align="left">
                                            </td>
                                            <td style="width: 1px; height: 20px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label91" runat="server" Width="105px" Text="Net Amount" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px; height: 20px" align="right">
                                                <asp:TextBox ID="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 70px">
                                                                <asp:Button AccessKey="C" ID="btnCalculate" OnClick="btnCalculate_Click" runat="server"
                                                                    Width="70px" Font-Size="8pt" Text="Calculate" Enabled="False" CssClass="Button"
                                                                    OnClientClick="showPopup()" />
                                                            </td>
                                                            <td style="width: 100px">
                                                                <asp:Button AccessKey="S" ID="btnSaveOrder" OnClick="btnSaveOrder_Click" runat="server"
                                                                    Width="100px" Font-Size="8pt" Text="Save Order" Enabled="False" CssClass="Button"
                                                                    OnClientClick="showPopup()" />
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:Button AccessKey="H" ID="btnUpdateOrder" Enabled="false" runat="server" Width="100px" Font-Size="8pt"
                                                                    Text="Update Order" OnClick="btnUpdateOrder_Click" CssClass="Button" />
                                                           
                                                                 <asp:Button AccessKey="H" ID="btnCancel" runat="server" Width="70px" Font-Size="8pt"
                                                                    Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="width: 1px" valign="top" align="left">
                                                &nbsp; &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 1px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="105px" Text="Cash Received" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 7px" align="right">
                                                <asp:TextBox ID="txtCashReceived" runat="server" Width="150px" ForeColor="Black"
                                                    Font-Bold="True"  Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                        <asp:TextBox ID="numTxtTotalSED" runat="server" CssClass="txtBox " Font-Bold="False"
                            ForeColor="Black" Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
