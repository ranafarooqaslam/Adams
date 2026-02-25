<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPysicalStockteken.aspx.cs" Inherits="Forms_frmPysicalStockteken"
    Title="SAMS :: Physical Stock Taking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;

           
            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }
            str = document.getElementById('<%=txtusaleableqty.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Un SaleQuantity');
                return false;
            }
            

            return true;
        }
        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";

        }
        function pageLoad() {
            $("select").searchable();

            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }
       
        function SearchSKUList(e) {
            var l = document.getElementById('<%= lstCode.ClientID %>');
            var tb = document.getElementById('<%= ddlSKuCde.ClientID %>');
            if (e.keyCode == 27) {
                document.getElementById("<%= txtQuantity.ClientID %>").focus();
            }
            else {
                if (tb.value == "") {
                    ClearSelection(l);
                }
                else {
                    for (var i = 0; i < l.options.length; i++) {
                        if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                            l.options[i].selected = true;
                            return false;
                        }
                        else {
                            ClearSelection(l);
                        }
                    }
                }
            }
        }

        function SearchSKUCode() {

            var str = document.getElementById("<%= lstCode.ClientID %>").value;

            if (str.length > 0) {

                document.getElementById("<%= txtQuantity.ClientID %>").focus();
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
               
            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= lstCode.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));

        }
        function SelectSkuCode(e) {

            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;

            if (key == 13) {
                e.preventDefault();

                var str = document.getElementById("<%= lstCode.ClientID %>").value;

                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";

                document.getElementById("<%= txtQuantity.ClientID %>").focus();
            }
        }


        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
     
        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }
        function jsIsUserFriendlyChar(val, step) {

            // Backspace, Tab, Enter, Insert, and Delete  
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, and Arrows  
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {  //Check dot key code should be allowed
                    return true;
                }
            }
            // The rest  
            return false;
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label Width="74px" CssClass="lblbox" ID="lblDocumentNo" runat="server" Text="Location"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList CssClass="DropList" ID="drpDistributor" runat="server" 
                                                    AutoPostBack="true" Width="200px" 
                                                    onselectedindexchanged="drpDistributor_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 316px" colspan="1" rowspan="2" valign="middle">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label CssClass="lblbox" ID="lbltoLocation" runat="server" Text="Principal" Width="73px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList AutoPostBack="True" CssClass="DropList"
                                                 ID="drpPrincipal" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                    runat="server" Width="200px" >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                             <td style="width: 316px" valign="middle" align="center" colspan="2" rowspan="7">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="239px" Height="8px" CssClass="lblbox"></asp:Label></strong>
                                                &nbsp; &nbsp;&nbsp;
                                                <asp:Panel ID="Panel1" runat="server" Width="250px" Height="170px" CssClass="HidePanel"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="Silver">
                                                    <strong>
                                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Width="170px">Select from SKU List</asp:Label></strong><br />
                                                    <asp:ListBox ID="lstCode" runat="server" Height="154px" onkeypress="SelectSkuCode(event)"
                                                        SelectionMode="Multiple" Width="245px"></asp:ListBox>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            &nbsp;</div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td style="height: 16px">
                                            
                                            <strong>
                                                <asp:Label ID="lblskuname" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="   Description" Width="290px"></asp:Label></strong>
                                        </td>
                                        <td style="height: 16px">
                                            <strong>
                                                <asp:Label ID="lblquantity" runat="server" BackColor="#006699" CssClass="lblbox"
                                                    Font-Bold="True" ForeColor="White" Height="16px" Text="Sale Qty" Width="81px"></asp:Label></strong>
                                        </td>
                                        <td style="height: 16px;">
                                            <strong>
                                                <asp:Label ID="lblFreeSKU" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Un Sale Qty" Width="81px"></asp:Label></strong>
                                        </td>
                                        <td style="height: 16px">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Unit Rate" Width="81px"></asp:Label></strong>
                                        </td>
                                        <td style="height: 16px">
                                            <strong>
                                                <asp:Label ID="Label41" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                                    ForeColor="White" Height="16px" Text="Add SKU" Width="96px"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 9px">
                                            <asp:DropDownList ID="ddlSKuCde" runat="server" Width="289px"  onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);" 
                                                     >
                                                </asp:DropDownList>
                                        </td>
                                        <td style="height: 9px">
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtBox" onfocus="SearchSKUCode()"
                                                Width="79px"  ></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtQuantity" ></ajaxToolkit:FilteredTextBoxExtender >
                                        </td>
                                        <td style="height: 9px;">
                                            <asp:TextBox ID="txtusaleableqty" runat="server" CssClass="txtBox " 
                                                Width="79px"  Text="0" ></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtusaleableqty" ></ajaxToolkit:FilteredTextBoxExtender >
                                        </td>
                                        <td style="height: 9px">
                                            <asp:TextBox ID="txtUnitRate" runat="server" CssClass="txtBox" Width="78px"  onkeydown="return jsDecimals(event);">0</asp:TextBox>
                                        </td>
                                        <td style="height: 9px">
                                            <asp:Button ID="btnSave" runat="server" AccessKey="A" Font-Size="8pt" OnClick="btnSave_Click"
                                                Text="Save" ValidationGroup="vg" Width="95px" CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="6">
                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px"
                                                Height="150px" ScrollBars="Vertical" Width="640px">
                                                <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                    OnRowDeleting="GrdPurchase_RowDeleting" OnRowCommand="GrdPurchase_RowCommand"
                                                    ShowHeader="False" Width="623px">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <RowStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                Width="85px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                Width="205px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SALEABLE_QUANTITY" HeaderText="Quantity">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="82px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UNSALEABLE_QUANTITY" HeaderText="PRICE">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                Width="82px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UNIT_RATE" HeaderText="UNIT_RATE" DataFormatString="{0:F2}">
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px"
                                                                HorizontalAlign="Right" />
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
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                    Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" />
                                                    <PagerStyle BackColor="Transparent" />
                                                    <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                    <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp; &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
