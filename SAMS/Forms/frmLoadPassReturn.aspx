<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmLoadPassReturn.aspx.cs" Inherits="Forms_frmLoadPassReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function CalcualeQty() {


        }
        function ValidateForm() {
            var str;



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


        }




        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }



        function CalculateSold() {


            var gv = document.getElementById('<%=GrdPurchase.ClientID %>');
            var tb = gv.getElementsByTagName("input");
            var Sold = 0;
            var objValue = 0;
            for (var i = 0; i < tb.length; i++) {

                if (tb[i].type == "text") {

                    if (tb[i].name.indexOf("txtRETURN_QUANTITY") !== -1) {

                        try {

                            var Sold = parseFloat(tb[i + 5].value) - parseFloat(tb[i].value);
                        
                            if (isNaN(Sold)) {
                                tb[i + 3].value = "";
                                
                            }
                            else {
                                if (parseFloat(Sold) >= 0) {

                                    if (tb[i + 4].checked == false) {

                                        tb[i + 3].value = Sold
                                    }
                                    else {
                                        tb[i + 3].value = "";
                                    }
                                } else {
                                    alert("Return Qty Should be Less than Issued !");
                                    tb[i].value = "";
                                    tb[i + 3].value = "";
                                    tb[i].focus();
                                }
                            }

                        }


                        catch (e) {

                        }

                    }
                }
            }

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
        <asp:UpdatePanel ID="up_hidden" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <div style="left: 120px; position: absolute; top: 150px;">
                                <div style="left: -100px; position: absolute; top: 70px; height: 248px;">
                                    <asp:Panel ID="Panel1" runat="server" Width="327px" Height="237px" BorderWidth="1px"
                                        BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                        <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                            border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                            <tbody>
                                                <tr>
                                                    <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                        &nbsp;<strong>Select SKU from List Press Enter</strong>
                                                    </td>
                                                    <td style="border-bottom: black thin solid" valign="top" align="right">
                                                        <asp:Button ID="Button5" runat="server" AccessKey="S" BorderStyle="Groove" BorderWidth="1px"
                                                            Font-Size="8pt" Height="16px" Text="X" Width="21px" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <asp:ListBox ID="lstCode" onkeydown="SelectSkuCode(event)" runat="server" Width="95%"
                                            Height="87%" SelectionMode="Multiple"></asp:ListBox>
                                    </asp:Panel>
                                    &nbsp;
                                </div>
                            </div>
                            <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 209px;
                                height: 100px">
                                <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="background-color: #FFFFCC;
                                    display: none; height: 50px; width: 85px; padding: 10px">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up_hidden">
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
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblWorkingDate" runat="server" Text="Working Date" Enabled="false"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px;">
                                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="196px" AutoPostBack="true"
                                            OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                            Format="dd-MMM-yyyy" PopupButtonID="ImgBntToDate" TargetControlID="txtToDate"
                                            PopupPosition="BottomLeft">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        <strong>
                                            <asp:Label ID="lblDocumentNo" runat="server" Text="Transaction No" Width="100px"></asp:Label></strong>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" colspan="1" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px; width: 238px;" align="left">
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px; width: 238px;" align="left">
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblfromLocation" runat="server" Width="120px" Text="Route" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px; width: 238px;" align="left">
                                        <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label6" runat="server" Width="94px" Text="Delivery Man" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px; width: 238px;" align="left">
                                        <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblCustomer" runat="server" Width="79px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="Label7" runat="server" Text="Vehicle No" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px;">
                                        <strong>
                                            <asp:Label ID="lblVehicleNo" runat="server" CssClass="lblbox"></asp:Label></strong>
                                        <asp:DropDownList ID="DrpVehicleNo" runat="server" Visible="false" CssClass="DropList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="80%">
                    <tr>
                        <td style="height: 173px;" valign="top">
                            <asp:Panel ID="Panel5" runat="server">
                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                <table style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                    border-bottom: silver thin solid">
                                    <tbody>
                                        <tr>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="lblskuname" runat="server" Width="250px" Height="16px" Font-Bold="True"
                                                    Text="Description"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label4" runat="server" Width="70px" Height="16px" Font-Bold="True"
                                                    Text="Sku Rate"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label2" runat="server" Width="70px" Height="16px" Font-Bold="True"
                                                    Text="Demand"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label3" runat="server" Width="70px" Height="16px" Font-Bold="True"
                                                    Text="Issue"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="lblquantity" runat="server" Width="70px" Height="16px" Font-Bold="True"
                                                    Text="Return"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label9" runat="server" Width="80px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Replacement"></asp:Label>
                                            </td>
                                            <td style="font-weight: bold;" class="tblhead">
                                                <asp:Label ID="Label1" runat="server" Width="100px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Purchase Return"></asp:Label>
                                            </td>
                                            <td style="font-weight: bold;" class="tblhead">
                                                <asp:Label ID="Label8" runat="server" Width="80px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Sold"></asp:Label>
                                            </td>
                                            <td style="font-weight: bold;" class="tblhead">
                                                <asp:Label ID="Label10" runat="server" Width="80px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Pending"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="9">
                                                <asp:Panel ID="Panel3" runat="server" Width="100%" Height="230px" ScrollBars="Vertical"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White"
                                                        ShowHeader="False" OnRowDataBound="GrdPurchase_RowDataBound">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <RowStyle ForeColor="Black"></RowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="50px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="200px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_RATE" HeaderText="SKU RATE" DataFormatString="{0:F2}">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="80px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DEMAND_QUANTITY" HeaderText="Demand">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="80px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ISSUED_QUANTITY" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="80px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Return">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRETURN_QUANTITY" runat="server" Width="75px" Text='<%# Eval("RETURN_QUANTITY")%>'></asp:TextBox>
                                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtRETURN_QUANTITY">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sale Return">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSALE_RETURN_QUANTITY" runat="server" Width="86px" Text='<%# Eval("SALE_RETURN_QUANTITY")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Purchase Returnn">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPURCHASE_RETURN_QUANTITY" runat="server" Width="106px" Text='<%# Eval("PURCHASE_RETURN_QUANTITY")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sold">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtBALANCE_QUANTITY" ReadOnly="true" runat="server" Width="85px"
                                                                        Text='<%# Eval("BALANCE_QUANTITY")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pending">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbIsPending" runat="server"   Checked='<%# Eval("Is_Pending") %>' />
                                                                </ItemTemplate>
                                                               
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="60px" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtISSUED_QUANTITY" runat="server" Width="85px" Text='<%# Eval("ISSUED_QUANTITY")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:TemplateField>
                                                             <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT DATE">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White"></FooterStyle>
                                                        <PagerStyle BackColor="Transparent"></PagerStyle>
                                                        <HeaderStyle CssClass="tblhead" ForeColor="White"></HeaderStyle>
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333">
                                                        </AlternatingRowStyle>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 100px; height: 21px">
                            <asp:Button ID="btnSaveOrder" runat="server" AccessKey="S" CssClass="Button" Font-Size="8pt"
                                Text="Save" Width="110px" OnClick="btnSaveOrder_Click" OnClientClick="showPopup()" />
                        </td>
                        <td style="width: 100px; height: 21px">
                            <asp:Button ID="btnCancel" runat="server" AccessKey="H" CssClass="Button" Font-Size="8pt"
                                Text="Cancel" Width="110px" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
