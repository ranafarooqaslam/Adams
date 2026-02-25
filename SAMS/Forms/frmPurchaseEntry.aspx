<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPurchaseEntry.aspx.cs" Inherits="Forms_frmPurchaseEntry" Title="SAMS :: Stock Register" %>

<%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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

            str = document.getElementById('<%=txtDocumentNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Invoice/DC No');
                return false;
            }

            str = document.getElementById('<%=txtBuiltyNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must Enter Builty No');
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
                           <ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup"
            BehaviorID="programmaticModalPopupBehavior"
            TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="programmaticPopup"
            BackgroundCssClass="modalBackground"
            DropShadow="True"
            RepositionMode="RepositionOnWindowScroll" >
        </ajaxToolkit:ModalPopupExtender>
          <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" style="display:none"/>
                        </div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="Label2" runat="server" Height="14px" 
                                                Text="Transaction Type" Width="117px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                            <asp:DropDownList ID="DrpDocumentType" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" Width="200px">
                                                <asp:ListItem Value="5">Transfer Out (Shop)</asp:ListItem>
                                                <asp:ListItem Value="-5">Transfer Out (Branch)</asp:ListItem>
                                                <asp:ListItem Value="3">Purchase Return</asp:ListItem>
                                                <asp:ListItem Value="4">Transfer In (Shop)</asp:ListItem>
                                                <asp:ListItem Value="-4">Transfer In(Branch)</asp:ListItem>
                                                <asp:ListItem Value="15">Returnable Replace Dispatch</asp:ListItem>
                                               <%-- <asp:ListItem Value="16">Returnable Stock Received</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lblAccountHead" runat="server" Height="14px" Text="Account Head" Width="98px" Visible="false"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:45%">
                                            <asp:DropDownList ID="ddlAccountHead" runat="server" Width="200px" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%">
                                         
                                        </td>
                                        <td style="width:45%">
                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Principal" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                            <asp:DropDownList ID="drpPrincipal" runat="server" AutoPostBack="True" CssClass="DropList"
                                                OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%">
                                         
                                        </td>
                                        <td style="width:45%">
                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Purchase For" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" CssClass="DropList"
                                                Width="200px" onselectedindexchanged="drpDistributor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%">
                                         
                                        </td>
                                        <td style="width:45%">
                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="Label4" runat="server" Text="Transfer To" Visible="False" Width="82px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                            <asp:DropDownList ID="DrpTransferFor" runat="server" CssClass="DropList" Visible="False"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%"></td>
                                        <td style="width:45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="INV/DC  No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                           <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%"></td>
                                        <td style="width:45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="Label3" runat="server" Text="Builty No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:25%">
                                           <asp:TextBox ID="txtBuiltyNo" runat="server" CssClass="txtBox" Width="195px"></asp:TextBox>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%"></td>
                                        <td style="width:45%"></td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <asp:CheckBox ID="ChbBatchNo" runat="server" AutoPostBack="True" OnCheckedChanged="ChbBatchNo_CheckedChanged"
                                                    Text="Batch No" Width="77px" />
                                        </td>
                                        <td style="width:25%">
                                           <asp:CheckBox ID="ChbFreeSKU" runat="server" Width="121px" Text="Apply Free SKU"
                                                    AutoPostBack="True" OnCheckedChanged="ChbFreeSKU_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td style="width:5%"></td>
                                        <td style="width:10%"></td>
                                        <td style="width:45%"></td>
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
                                            <td>
                                                 <asp:Label ID="lblskuname" runat="server" Width="300px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text=" Description" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="73px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFreeSKU" runat="server" Width="75px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Free SKU" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                <asp:Label ID="lblBatchNo" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Batch No" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label41" runat="server" Width="100%" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                              <asp:DropDownList ID="ddlSKuCde" runat="server" Width="299px"  onfocus="ddlFocus(this);" 
                                                    onblur="ddlBlur(this);" 
                                                  >
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" onfocus="SearchSKUCode();"   runat="server" Width="70px"
                                                    CssClass="txtBox "></asp:TextBox>

                                                    <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtQuantity" ></ajaxToolkit:FilteredTextBoxExtender >
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFreeSKU" runat="server" Width="70px" CssClass="txtBox" Enabled="False">0</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat ="server" FilterType="Numbers" TargetControlID ="txtFreeSKU"></ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBatchNo" runat="server" Width="76px" CssClass="txtBox" Enabled="False">N/A</asp:TextBox>
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
                                                            <asp:BoundField DataField="Quantity" ReadOnly="true" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FREE_SKU" ReadOnly="true" HeaderText="Free SKU">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" ReadOnly="true" HeaderText="BatchNo">
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
                                    </tbody>
                                </table>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document"  OnClick="btnSaveDocument_Click" OnClientClick="showPopup()"
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
