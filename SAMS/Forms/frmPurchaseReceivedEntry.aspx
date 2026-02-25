<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseReceivedEntry.aspx.cs" Inherits="Forms_frmPurchaseReceivedEntry"
    Title="SAMS :: Goods Receipt Note [GRN]" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript">
        function showPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.show();
        }
        function hidepopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();

        }
        function pageLoad()
        {
            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 209px;
                                    height: 100px">
                                    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="background-color: #FFFFCC;
                                        display: none; height: 50px; width: 85px; padding: 10px">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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
                                <table width="100%">
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Purchase For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            <strong>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text="P.O. Ref #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 84%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--  </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>--%>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel2" runat="server" Width="980px" Height="250px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%">
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
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="230px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Ord Qty">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Prev Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="75px" Text='<%# Eval("RcdQty")%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Curr Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentRecQty" runat="server" Width="75px" Text='<%# Eval("CurrentRcdQty")%>'></asp:TextBox>
                                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Prev Free Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFreeRecQty" runat="server" Width="60px" Text='<%# Eval("FreeRcdQty")%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Curr Free Rcd Qty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentFreeRecQty" runat="server" Width="60px" Text='<%# Eval("CurrentFreeRcdQty")%>'></asp:TextBox>
                                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentFreeRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Exp. Date">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtExpDate" runat="server" Width="75%" Text='<%# Eval("ExpDate")%>'
                                                                        onkeyup="BlockStartDateKeyPress()" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                    <asp:ImageButton ID="ibExpDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                        Width="16px" />
                                                                    <ajaxToolkit:CalendarExtender ID="ceExpDate" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ibExpDate" TargetControlID="txtExpDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mfg. Date">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMfgDate" runat="server" Width="75%" Text='<%# Eval("MfgDate")%>'
                                                                        onkeyup="BlockStartDateKeyPress()" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                    <asp:ImageButton ID="ibMfgDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                                        Width="16px" />
                                                                    <ajaxToolkit:CalendarExtender ID="ceMfgDate" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ibMfgDate" TargetControlID="txtMfgDate">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="PRICE">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <FooterStyle BackColor="White" />
                                                        <PagerStyle BackColor="Transparent" />
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="740px">
                        <tr>
                            <td>
                                <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label7" runat="server" Width="100px" Height="16px" Text="Total Quantity"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="80px" CssClass="txtBox"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="740px" style="border-style: solid; border-width: 1px; border-color: #C0C0C0;">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label1" runat="server" Height="16px" Width="100px" Text="Dispatch Date"></asp:Label></strong>
                                                        <asp:TextBox ID="txtDispatchDate" runat="server" CssClass="txtBox" MaxLength="10"
                                                            onkeyup="BlockStartDateKeyPress()" Width="200px"></asp:TextBox>
                                                        <asp:ImageButton ID="ibtnDispatchDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                            Width="16px" />&nbsp;
                                                        <asp:Label Width="50px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label2" runat="server" Width="100px" Height="16px" Text="Dispatch No"></asp:Label></strong>
                                                        <asp:TextBox ID="txtDispatchNo" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label3" runat="server" Width="100px" Height="16px" Text="Seal No"></asp:Label></strong>
                                                        <asp:TextBox ID="txtSealNo" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label4" runat="server" Width="100px" Height="16px" Text="Gate Pass No"></asp:Label></strong>
                                                        <asp:TextBox ID="txtGatePassNo" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label5" runat="server" Width="100px" Height="16px" Text="Driver Name"></asp:Label></strong>
                                                        <asp:TextBox ID="txtDriverName" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label6" runat="server" Width="100px" Height="16px" Text="Vehicle No"></asp:Label></strong>
                                                        <asp:TextBox ID="txtVehicleNo" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="Label8" runat="server" Width="100px" Height="16px" Text="Temperature"></asp:Label></strong>
                                                        <asp:TextBox ID="txtTemperature" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height: 5px;">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
                <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
                <ajaxToolkit:CalendarExtender ID="CEDispatchDate" runat="server" Format="dd-MMM-yyyy"
                    PopupButtonID="ibtnDispatchDate" TargetControlID="txtDispatchDate">
                </ajaxToolkit:CalendarExtender>
                <div>
                    <table>
                        <tr>
                            <td>
                                <br />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" OnClientClick="showPopup()" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hf_ID" runat="server" />
                    <asp:HiddenField ID="hf_PID" runat="server" />
                     <asp:HiddenField ID="hfPrincipalName" runat="server" />
                    <asp:HiddenField ID="hf_CurrentRcdQty" runat="server" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveDocument" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
