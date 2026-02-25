<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDelieveryOrderAdd.aspx.cs" Inherits="Forms_frmDelieveryOrderAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
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
        <asp:UpdatePanel ID="pnlMaster" runat="server">
            <ContentTemplate>

                <asp:HiddenField runat="server" ID="hfOrderQty" />
                <asp:HiddenField runat="server" ID="hfIsGstRegister" />
                <table cellpadding="2px">
                    <tr class="tblhead">
                        <td class="tblhead" style="width: 400px;">
                            <strong>
                                <asp:Label runat="server" ID="lblCustomer" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong style="font-size:medium">PO Date:
                                <asp:Label runat="server" ID="lblDate" ></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblDONo" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblOrderNo" Font-Size="Medium"></asp:Label></strong>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 65px;">
                            <strong>
                                <label>
                                    Location</label>
                            </strong>
                        </td>
                        <td>
                            <asp:Label ID="ddlDistributor" runat="server" Width="231px" Font-Size="Medium">
                            </asp:Label>
                            <asp:HiddenField ID="hfDistributor" runat="server" />
                            <%-- for warehouse--%>
                            <asp:HiddenField ID="hfLocation" runat="server" />
                            <%--for Distributor--%>
                            <asp:HiddenField ID="hfPrincipal" runat="server" />
                            <asp:HiddenField ID="hfCustomer" runat="server" />
                            <asp:CheckBox ID="ChbDiscount" runat="server" Visible="False" Width="90px" Text="Promotion"
                                Checked="True"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Order Type</strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RblPayMode" runat="server" Width="250px" Height="1px" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
                                <asp:ListItem Value="215">Credit</asp:ListItem>
                                <asp:ListItem Value="216">Advance</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td style="width: 15%;">
                                        <strong>
                                            <asp:Label ID="Label13" runat="server" Width="90px" Text="Credit Ceiling"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtCreditLimit" runat="server" Width="76px"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        <strong>
                                            <asp:Label ID="Label14" runat="server" Width="80px" Text="Credit Used"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtCreditUsed" runat="server" Width="110px"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        <strong>
                                            <asp:Label ID="Label15" runat="server" Width="90px" Text="Credit Balance"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtBalance" runat="server" Width="107px"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtPriceGroup" runat="server" Width="105px" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="txtDiscountType" runat="server" Width="90px"
                                            Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlDetail" runat="server">
            <ContentTemplate>
              
                <table width="100%">
                    <tr valign="top">
                        <td style="width: 72%">
                            <fieldset style="width: 200px;">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Width="645px" Height="220px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="SteelBlue" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                                        OnRowDataBound="grdOrder_RowDataBound">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="440px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Order Qty">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Prev Dispatch Qty">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="75px" Text='<%# Eval("RcdQty")%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dispatch Qty">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentRecQty" runat="server" Width="75px" Text='<%# Eval("CurrentRcdQty")%>'
                                                                        OnTextChanged="txtCurrentRecQty_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="AMOUNT" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RcdQty">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CurrentRcdQty">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="STANDARD_DISCOUNT" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>

                                                            <asp:BoundField DataField="NET_AMOUNT" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 28%">
                            <table>

                                <tr >
                                    <td colspan="2">
                                        &nbsp;
                                        <fieldset style="display:none;">
                                            <table>
                                                <tr>
                                                    <td style="width: 200px; font-size: 16px;">
                                                        <strong>Ledger Balance:</strong>
                                                    </td>
                                                    <td style="font-size: 16px;">
                                                        <asp:Label ID="lblLedgerBalance" runat="server"></asp:Label>&nbsp;
                                                        <asp:Label ID="lblOpeningType" runat="server"></asp:Label>
                                                    </td>
                                                </tr>


                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Gross Sale </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrossAmount" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Extra Discount </strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numtxtTotalExtraDiscnt" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Standard Discount</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numTxtTotalStndrdDiscnt" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>S. Tax Amount</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numTxtTotalGST" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Add. Tax Amount</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numTxtTotalTST" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Net Amount</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="numTxtTotlAmnt" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Cash Received</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCashReceived" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                

                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>


        <table>
            <tr>

                <td>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td valign="top" align="left" colspan="2" rowspan="7">
                                            <strong>
                                                <asp:Label ID="Label10" runat="server" Width="60px" Text="Free SKU"></asp:Label></strong><asp:Panel
                                                    ID="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove"
                                                    BorderWidth="1px">
                                                    <asp:GridView ID="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2"
                                                        BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
                                                        <RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black"></RowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
                                                                <ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Qty">
                                                                <ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                    </asp:GridView>
                                                </asp:Panel>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
                <td colspan="4">

                    <asp:Button AccessKey="C" ID="btnCalculate" runat="server" OnClick="btnCalculate_Click"
                        Width="100px" Font-Size="8pt" Text="Calculate" CssClass="Button"
                        OnClientClick="showPopup()" />

                    <asp:Button AccessKey="S" ID="btnSaveOrder" runat="server" Width="100px" Font-Size="8pt"
                        Text="Save" UseSubmitBehavior="False" OnClick="btnSaveOrder_Click" CssClass="Button" />
                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                        Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
