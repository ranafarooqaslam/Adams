<%@ Page Title="Gift SKU Entry" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmGiftSKU2.aspx.cs" Inherits="Forms_frmGiftSKU2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
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

            var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
            unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
            document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;

            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                document.getElementById("<%= txtUnitRate.ClientID %>").value = unitprice;
            });

            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }

       
    </script>
    <script type="text/javascript">
        function showPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
        function hidepopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
        }
    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 350px;
        }
    </style>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 209px;
                        height: 100px">
                        <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="background-color: #FFFFCC;
                            display: none; height: 50px; width: 85px; padding: 10px">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <div id='messagediv' style="text-align: center">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="27px" />
                                        Wait Update.......
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                            TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="programmaticPopup"
                            BackgroundCssClass="modalBackground" DropShadow="True" RepositionMode="RepositionOnWindowScroll">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                    </div>
                    <tr>
                        <td align="left">
                            <strong>
                                <asp:Label ID="lblDocumentNo" runat="server" Width="79px" Text="Document No" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpDocumentNo" runat="server" Width="280px" CssClass="DropList"
                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <strong>
                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="drpDistributor" runat="server" Width="280px" CssClass="DropList"
                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <strong>
                                <asp:Label ID="Label5" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label>
                            </strong>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="280px" CssClass="DropList"
                                OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <strong>
                                <asp:Label ID="Label1" runat="server" Width="79px" Text="Account Head" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="DrpAccountHead" runat="server" Width="280px" onfocus="ddlFocus(this);"
                                onblur="ddlBlur(this);">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <strong>
                                <asp:Label ID="Label6" runat="server" Width="79px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRemarks" runat="server" Width="200px" CssClass="txtBoxnNum"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="height: 16px">
                            <asp:Label ID="lblskuname" runat="server" Width="260px" Height="16px" Font-Bold="True"
                                Text="   Description" BackColor="#006699" ForeColor="White"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="Label3" runat="server" Width="61px" Height="16px" ForeColor="White"
                                Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="lblquantity" runat="server" Width="62px" Height="16px" ForeColor="White"
                                Font-Bold="True" Text="Pur. Price" CssClass="lblbox" BackColor="#006699"></asp:Label>
                        </td>
                        <td style="height: 16px" align="center">
                            <asp:Label ID="Label7" runat="server" Width="100%" Height="16px" ForeColor="White"
                                Font-Bold="True" Text="GST Rate" CssClass="lblbox" BackColor="#006699"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="Label2" runat="server" Width="100%" Height="16px" ForeColor="White"
                                Font-Bold="True" Text="Amount" CssClass="lblbox" BackColor="#006699"></asp:Label>
                        </td>
                        <td style="height: 16px">
                            <asp:Label ID="Label4" runat="server" Width="100%" Height="16px" ForeColor="White"
                                Font-Bold="True" Text="Add SKU" BackColor="#006699"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                            <asp:DropDownList ID="ddlSKuCde" runat="server" Width="260px" onfocus="ddlFocus(this);"
                                onblur="ddlBlur(this);">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtQuantity" onfocus="SearchSKUCode();" runat="server" Width="55px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                FilterType="Numbers" TargetControlID="txtQuantity">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtUnitRate" runat="server" Width="56px" CssClass="txtBox" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtGstRate" runat="server" Width="62px" CssClass="txtBoxnNum"></asp:TextBox>
                        </td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtAmount" runat="server" Width="72px" CssClass="txtBoxnNum" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="height: 21px">
                            <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="7">
                            <asp:Panel ID="Panel2" runat="server" Width="640px" Height="130px" ScrollBars="Vertical"
                                BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                <asp:GridView ID="GrdSKU" runat="server" ForeColor="SteelBlue" CssClass="gridRow2"
                                    BackColor="White" BorderColor="White" ShowHeader="False" OnRowDeleting="GrdSKU_RowDeleting"
                                    OnRowCommand="GrdSKU_RowCommand" HorizontalAlign="Center" AutoGenerateColumns="False">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous" />
                                    <RowStyle ForeColor="Black" />
                                    <Columns>
                                        <asp:BoundField DataField="GIFT_MASTER_ID" HeaderText="GIFT_MASTER_ID">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                            <HeaderStyle CssClass="HidePanel" />
                                            <ItemStyle CssClass="HidePanel" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                Width="75px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                Width="65px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UNIT_PRICE" DataFormatString="{0:F2}" HeaderText="PRICE">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                Width="65px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GST_RATE" HeaderText="GST Rate">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" DataFormatString="{0:F2}" HeaderText="Amount">
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right"
                                                Width="80px" />
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
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="Delete"></asp:LinkButton>
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
           
            </ContentTemplate>
           
        </asp:UpdatePanel>
             <table>
                    <tr>
                        <td style="width: 100px">
                            <asp:Button AccessKey="S" ID="btnSaveOrder" OnClick="btnSaveOrder_Click" runat="server"
                                Width="100px" Font-Size="9pt" Text="Save" CssClass="Button" OnClientClick="showPopup()" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button AccessKey="H" ID="btnCancel" runat="server" Width="100px" Font-Size="9pt"
                                Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                        </td>
                    </tr>
                </table>
    </div>
</asp:Content>
