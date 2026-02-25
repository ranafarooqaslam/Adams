<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    Title="SAMS :: Load Pass Issue" CodeFile="frmLoadPass.aspx.cs" Inherits="Forms_frmLoadPass" %>

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
            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var Return = document.getElementById('<%=txtReturn.ClientID%>').value;
            document.getElementById("<%= txtSold.ClientID %>").value = (Qty - Return);

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
            document.getElementById("<%= txtskuRate.ClientID %>").value = unitprice;

            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                document.getElementById("<%= txtskuRate.ClientID %>").value = unitprice;
            });


            var Str = $('#<%=DrpCustomer.ClientID %> option:selected').text();

            var unitprice = Str.substring(Str.indexOf('|') + 1);

            document.getElementById("<%= txtPriceGroup.ClientID %>").value = unitprice;


            $('#<%=DrpCustomer.ClientID %>').change(function () {

                var Str = $('#<%=DrpCustomer.ClientID %> option:selected').text();

                var unitprice = Str.substring(Str.indexOf('|') + 1);

                document.getElementById("<%= txtPriceGroup.ClientID %>").value = unitprice;


            });

        }




        function ClearSelection(lb) {
            lb.selectedIndex = -1;
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
                                        <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="175px" AutoPostBack="true"
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
                                        <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
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
                                        <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" CssClass="DropList"
                                            AutoPostBack="True" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="Label5" runat="server" Width="94px" Text="Delivery Man" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px; width: 238px;" align="left">
                                        <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="200px" CssClass="DropList"
                                            OnSelectedIndexChanged="DrpDeliveryMan_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <strong>
                                            <asp:Label ID="lblCustomer" runat="server" Width="79px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                    </td>
                                    <td style="height: 25px" align="left">
                                        <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" 
                                            CssClass="DropList" onselectedindexchanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    
                                     <asp:TextBox ID="txtPriceGroup" runat="server" Width="105px"  
                                               ></asp:TextBox>
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
                            <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                <table style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid;
                                    width: 650px; border-bottom: silver thin solid">
                                    <tbody>
                                        <tr>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="lblskuname" runat="server" Width="320px" Height="16px" Font-Bold="True"
                                                    Text="   Description"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label6" runat="server" Width="61px" Height="16px" Font-Bold="True"
                                                    Text="SKU Rate"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label8" runat="server" Width="51px" Height="16px" Font-Bold="True"
                                                    Text="Demand"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label3" runat="server" Width="50px" Height="16px" Font-Bold="True"
                                                    Text="Issued"></asp:Label>
                                            </td>
                                            <%--   <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="lblquantity" runat="server" Width="50px" Height="16px" Font-Bold="True"
                                                    Text="Return"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label2" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Sold"></asp:Label>
                                            </td>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label9" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Replacement"></asp:Label>
                                            </td>
                                             <td style="font-weight:bold;" class="tblhead" >
                                             Purchase Return
                                                
                                            </td>
                                            --%>
                                            <td style="height: 16px" class="tblhead">
                                                <asp:Label ID="Label4" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Add SKU"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlSKuCde" runat="server" Width="320px" onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);" Style="z-index: 0;">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtskuRate" runat="server" Width="70px" Font-Bold="True" CssClass="txtBox"
                                                    Enabled="False"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDemand" onfocus="SearchedCode()" runat="server" Width="70px"
                                                    CssClass="txtBox "></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_Demand" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txtDemand">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txtQuantity" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txtQuantity">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" runat="server" Width="100px" Font-Size="8pt"
                                                    Text="Add Sku" ValidationGroup="vg" CssClass="Button" OnClick="btnSave_Click">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:TextBox ID="txtReturn" runat="server" onblur="CalcualeQty();" Visible="false">0</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txtReturn" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txtReturn">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:TextBox ID="txtSold" runat="server" CssClass="txtBoxnNum" Enabled="False" Visible="false">0</asp:TextBox>
                                                <asp:TextBox ID="txtSalesReturn" runat="server" CssClass="txtBoxnNum" Visible="false">0</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txtSalesReturn" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txtSalesReturn">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <asp:TextBox ID="txtPurchaseReturn" runat="server" CssClass="txtBoxnNum" Visible="false">0</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txtPurchaseReturn" runat="server" ValidChars="0123456789"
                                                    TargetControlID="txtPurchaseReturn">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Panel ID="Panel3" runat="server" Width="100%" Height="130px" ScrollBars="Vertical"
                                                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White"
                                                        ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" OnRowCommand="GrdPurchase_RowCommand"
                                                        Width="100%">
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
                                                                    Width="75px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="225px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_RATE" HeaderText="SKU RATE" DataFormatString="{0:F2}">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="90px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DEMAND_QUANTITY" HeaderText="Demand">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ISSUED_QUANTITY" HeaderText="Quantity">
                                                                <ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                                                    Width="65px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RETURN_QUANTITY" HeaderText="Return">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BALANCE_QUANTITY" HeaderText="Sold">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SALE_RETURN_QUANTITY" HeaderText="Sale Return">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PURCHASE_RETURN_QUANTITY" HeaderText="Purchase Return">
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
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        CommandName="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
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
