<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmReturnableReplaceDispatch.aspx.cs" Inherits="Forms_frmReturnableReplaceDispatch" Title="SAMS :: Returnable Replace Dispatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>

    <script language="JavaScript" type="text/javascript">
        function CheckDispatchQty() {
            debugger;
            var gv = document.getElementById('<%=GrdPurchase.ClientID %>');
            var tb = gv.getElementsByTagName("input");
            for (var i = 0; i < tb.length; i++) {
                if (tb[i].type == "text") {
                    if (tb[i].name.indexOf("txtCurrentDispatchQty") !== -1) {
                        var CurrentQty = parseFloat(tb[i].value);
                        var RemainigQty = parseFloat(tb[i + 1].value);
                        var PrevQty = parseFloat(tb[i + 2].value);
                        if(parseInt(CurrentQty) > parseInt(PrevQty) + parseInt(RemainigQty))
                        {
                            alert('Dispatch quantity should not be greater then received quantity');
                            tb[i].value = tb[i + 3].value;
                            return;
                        }
                    }
                }
            }
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
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 350px;
        }
    </style>
    <div id="right_data">
        <div>
            <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px; height: 100px">
                <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="background-color: #FFFFCC; display: none; height: 50px; width: 85px; padding: 10px">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                        <ProgressTemplate>
                            <div id='messagediv' style="text-align: center">
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="27px" />
                                Wait Update.......
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup"
                    BehaviorID="programmaticModalPopupBehavior"
                    TargetControlID="hiddenTargetControlForModalPopup"
                    PopupControlID="programmaticPopup"
                    BackgroundCssClass="modalBackground"
                    DropShadow="True"
                    RepositionMode="RepositionOnWindowScroll">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
            </div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">                                    
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 45%"></td>
                                    </tr>
                                    <tr >
                                        <td style="width: 10%">                                            
                                            <strong>
                                                <asp:Label ID="lblAccountHead" runat="server" Height="14px" Text="Account Head" Width="98px" Visible="false"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlAccountHead" runat="server" Width="200px" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Height="14px" Text="Transaction Type" Width="117px" Visible="false"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 45%">
                                            <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" Visible="false">
                                                <asp:ListItem Value="15">Returnable Replace Dispatch</asp:ListItem>
                                            </asp:DropDownList>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Principal" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" CssClass="DropList" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Purchase For" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                Width="200px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Transfer To" Visible="False" Width="82px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="DrpTransferFor" runat="server" CssClass="DropList" AutoPostBack="true"
                                                OnSelectedIndexChanged="DrpTransferFor_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="INV/DC  No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Builty No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 10%"></td>
                                        <td style="width: 45%"></td>
                                    </tr>                                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="680px" Height="250px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" Width="90%">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" ReadOnly="true" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" ReadOnly="true" HeaderText="SKU Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="85px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" ReadOnly="true" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="205px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Pending Dispatch">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentDispatchQty" runat="server" Width="90%" Text='<%# Eval("CurrentDispatchQty")%>' style="text-align:right" onblur="CheckDispatchQty();"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentDispatchQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="100px"/>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RemainingDispatchQty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRemainingDispatchQty" runat="server" Width="90%" Text='<%# Eval("RemainingDispatchQty")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:TemplateField>                                                            
                                                            <asp:TemplateField HeaderText="PrevDispatchQty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPrevDispatchQty" runat="server" Width="90%" Text='<%# Eval("PrevDispatchQty")%>'></asp:TextBox>                                                                    
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CurrentDispatchQty2">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentDispatchQty2" runat="server" Width="90%" Text='<%# Eval("CurrentDispatchQty")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="PRICE" ReadOnly="true" HeaderText="PRICE">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
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
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" OnClick="btnSaveDocument_Click" OnClientClick="showPopup()"
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                                <strong>
                                    <asp:Label ID="Label7" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong>
                                <asp:TextBox ID="txtTotalQuantity" onkeyup="SearchList()" runat="server" Width="88px"
                                    CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveDocument" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>