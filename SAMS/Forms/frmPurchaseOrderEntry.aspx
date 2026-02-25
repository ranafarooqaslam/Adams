<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseOrderEntry.aspx.cs" Inherits="Forms_frmPurchaseOrderEntry"
    Title="SAMS :: Purchase Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function calendarShown(sender) {
            sender._popupBehavior._element.style.zIndex = 10005;
        }
        function CalcualeAmount() {
            var Qty = document.getElementById('<%=txtQuantity.ClientID%>').value;
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value;

            document.getElementById("<%= txtAmount.ClientID %>").value = (Qty * Rate);
        }

        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtQuantity.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Quantity');
                return false;
            }


            str = document.getElementById('<%=txtRate.ClientID%>').value;

            if (str == null || str.length == 0) {

                alert('Must Enter Rate');

                return false;

            }
            str = document.getElementById('<%=txtRefNo.ClientID%>').value;

            if (str == null || str.length == 0) {

                alert('Must Enter Ref #');

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
            document.getElementById("<%= txtRate.ClientID %>").value = unitprice;

            $('#<%=ddlSKuCde.ClientID %>').change(function () {
                var unitprice = $('#<%=ddlSKuCde.ClientID %> option:selected').text();
                unitprice = unitprice.substring(unitprice.indexOf(':') + 1);
                document.getElementById("<%= txtRate.ClientID %>").value = unitprice;
            });

            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }



        function SearcSKUList(e) {
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
            var stroption = document.getElementById("<%= ddlSKuCde.ClientID %>").value;
            if (str.length > 0) {

                document.getElementById("<%= txtRate.ClientID %>").value = str.substring(str.indexOf(':') + 1);
                document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
               

            }
            else if (stroption.length == 0) {
                document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= lstCode.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));

        }
        function SelectSkuCode(e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var str = document.getElementById("<%= lstCode.ClientID %>").value;
                document.getElementById("<%= txtRate.ClientID %>").value = str.substring(str.indexOf(':') + 1);
                document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";

                document.getElementById("<%= txtQuantity.ClientID %>").focus();
            }
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
 <style type="text/css" >
            .modalBackground {
 background-color:Gray;
 filter:alpha(opacity=70);
 opacity:0.7;

}

.modalPopup {
 background-color:#ffffdd;
 border-width:3px;
 border-style:solid;
 border-color:Gray;
 padding:3px;
 width:350px;
}
    </style>
    <div id="right_data">
        <div>
         <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px;
                            height: 100px">
                            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" style="background-color:#FFFFCC;display:none;height:50px;width:85px;padding:10px">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                   
                                    <ProgressTemplate>
                                      <div id='messagediv' style="text-align:center">
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="27px" />
                                        Wait Update.......
                                        </div>
                                         </ProgressTemplate>
                                       
                                </asp:UpdateProgress>
                                 


                            </asp:Panel>
                           <cc1:ModalPopupExtender runat="server" ID="programmaticModalPopup"
            BehaviorID="programmaticModalPopupBehavior"
            TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="programmaticPopup"
            BackgroundCssClass="modalBackground"
            DropShadow="True"
            RepositionMode="RepositionOnWindowScroll" >
        </cc1:ModalPopupExtender>
          <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
                        </div>
            <table width="100%">
                <tr>
                    <td>
                     <div style="left: 15px; position: absolute; top: 200px; height: 275px;">
                            <asp:Panel ID="Panel3" runat="server" Width="327px" Height="237px" BorderWidth="1px"
                                BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                                <table style="border-right: #ffffff thin groove; border-top: #ffffff thin groove;
                                    border-left: #ffffff thin groove; width: 99%; border-bottom: #ffffff thin groove">
                                    <tbody>
                                        <tr>
                                            <td style="border-bottom: black thin solid" align="left" colspan="2">
                                                &nbsp;<strong>Select SKU from List Press Enter</strong>
                                            </td>
                                            <td style="border-bottom: black thin solid" valign="top" align="center">
                                                <asp:Button ID="Button5" runat="server" AccessKey="S" BorderStyle="Groove" BorderWidth="1px"
                                                    Font-Size="8pt" Height="23px" Text="X" Width="25px" CssClass="Button" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:ListBox ID="lstCode" onkeydown="SelectSkuCode(event)" runat="server" Width="95%"
                                    Height="87%" SelectionMode="Multiple"></asp:ListBox>
                            </asp:Panel>
                            &nbsp;
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                            <asp:Label ID="Label3" runat="server" Height="13px" Text="Date:" Width="76px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                        <asp:TextBox ID="txtPODate" runat="server" CssClass="txtBox" MaxLength="10"  
                                            Width="180px" AutoPostBack="true" ontextchanged="txtPODate_TextChanged"></asp:TextBox>
                                        <asp:ImageButton ID="ibtnDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                            Width="16px" />
                                          
                                        <cc1:CalendarExtender ID="CEDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnDate" OnClientShown="calendarShown"
                                            TargetControlID="txtPODate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblfromLocation" runat="server" Text="Order For:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" Width="200px" 
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" 
                                             AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblPrincipal" runat="server" Text="Principal:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"
                                                Width="200px" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblRefNo" runat="server" Text="Reference #:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 89%">
                                            <asp:TextBox ID="txtRefNo" runat="server" Width="195px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <strong>
                                                <asp:Label ID="lblCurrencty" runat="server" Text="Builty No:"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1%">

                                        </td>
                                        <td style="width: 89%">
                                             <asp:TextBox ID="txtBuiltNo" runat="server" Width="195px" ></asp:TextBox>
                                        </td>
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
                                          <td style="height: 16px">
                                           
                                                <asp:Label ID="lblskuname" runat="server" Width="300px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text=" Description" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="73px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFreeSKU" runat="server" Width="75px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Rate" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Width="79px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Amount" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                         <td>
                                               <asp:DropDownList ID="ddlSKuCde" runat="server" Width="299px"  onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" >
                                                </asp:DropDownList>
                                            </td>
                                            
                                            <td>
                                                <asp:TextBox ID="txtQuantity" onfocus="SearchSKUCode()" runat="server" Width="70px"  Onblur="CalcualeAmount();" ></asp:TextBox>
                                                <%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers"  TargetControlID="txtQuantity">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRate" runat="server" Width="70px" Enabled="false"  ></asp:TextBox>
                                                
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="76px" Enabled="false" >0.00</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="100px"
                                                    Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="6">
                                                <asp:Panel ID="Panel2" runat="server" Width="640px" Height="130px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowCommand="GrdPurchase_RowCommand"
                                                        ShowHeader="False" Width="620px">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                              <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="286px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRICE" HeaderText="Rate" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                              
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
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
                                        <tr>
                                            <td colspan="6">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Height="16px" Text="Total Quantity"></asp:Label></strong>
                                                <asp:TextBox ID="txtTotalQuantity" runat="server" Width="88px" ReadOnly="True" ></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<strong><asp:Label ID="Label1" runat="server" Height="16px"
                                                    Text="Amount"></asp:Label></strong>
                                                <asp:TextBox ID="txtAmountFC" runat="server" Width="120px" ReadOnly="True"></asp:TextBox>
                                               
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click"
                                    CssClass="Button" OnClientClick="showPopup()" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveDocument"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
