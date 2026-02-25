<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmOpeningStock.aspx.cs" Inherits="Forms_frmOpeningStock" Title="SAMS :: Stock Adjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
 <script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">
        function ConfirmDelete() {
            if (confirm("Do you want to Cancel this record?") == true)
                return true;

            else {
                return false;
            }
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
     .lblbox
     {}
    </style>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                   <div style="z-index: 101; left: 597px; width: 100px; position: absolute; top: 109px;
                            height: 100px">
                            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" style="background-color:#FFFFCC;display:none;height:50px;width:85px;padding:10px">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                   
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

                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>                                        
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="123px" Height="14px" Text="Transaction Type"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList"  onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged" >
                                                    <asp:ListItem Value="7">Opening Stock</asp:ListItem>
                                                    <asp:ListItem Value="8">Short</asp:ListItem>
                                                    <asp:ListItem Value="9">Excess</asp:ListItem>
                                                    <asp:ListItem Value="6">Damage</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">                                                
                                            </td>
                                        </tr>
                                         <tr>
                                            <td >
                                                <strong>
                                                    <asp:Label ID="lblAccountHead" runat="server" Height="14px" Text="Account Head" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td >
                                                <asp:DropDownList ID="ddlAccountHead" runat="server" Width="200px" onfocus="ddlFocus(this);" onblur="ddlBlur(this);" Visible="false">                                                    
                                                </asp:DropDownList>
                                            </td>
                                            <td>                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Width="94px" Text="Document No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDocumentNo" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"   onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location" ></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox" ></asp:TextBox>
                                            </td>
                                            <td style="height: 25px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" valign="top" align="left">
                                                <asp:CheckBox ID="ChbBatchNo" runat="server" Width="77px" Text="Batch No" AutoPostBack="True" >
                                                </asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 25px" valign="top">
                                                <asp:CheckBox ID="ChbFreeSKU" runat="server" Width="146px" Text="Apply Free SKU" 
                                                    AutoPostBack="True"></asp:CheckBox>
                                            </td>
                                            <td style="width: 1px; height: 25px" valign="top">
                                            </td>
                                        </tr>
                                    </tbody>
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                               
                                                <asp:Label ID="lblskuname" runat="server" Width="290px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Description" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="76px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFreeSKU" runat="server" Width="77px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Free SKU" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBatchNo" runat="server" Width="82px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Batch No" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label41" runat="server" Width="92px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                             <asp:DropDownList ID="ddlSKuCde" runat="server" Width="288px"  onfocus="ddlFocus(this);"
                                                    onblur="ddlBlur(this);" >
                                                </asp:DropDownList>
                                               
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="70px"  onfocus="SearchedCode();"
                                                    CssClass="txtBox "></asp:TextBox>
                                                      <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtQuantity" ></ajaxToolkit:FilteredTextBoxExtender >
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFreeSKU" runat="server" Width="70px" CssClass="txtBox" 
                                                 Enabled="False">0</asp:TextBox>
                                                   <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtFreeSKU" ></ajaxToolkit:FilteredTextBoxExtender >
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBatchNo" runat="server" Width="76px" CssClass="txtBox" Enabled="False">N/A</asp:TextBox>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="95px"
                                                    Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:Panel ID="Panel2" runat="server" Width="640px" Height="140px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="620px" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" BorderColor="White" BackColor="White" ShowHeader="False"
                                                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowCommand="GrdPurchase_RowCommand" HorizontalAlign="Center" AutoGenerateColumns="False">
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
                                                                    Width="70px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                                    Width="200px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="FREE_SKU" HeaderText="Free SKU">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BATCH_NO" HeaderText="BatchNo">
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
                                <asp:Button AccessKey="S" ID="btnSaveDocument" OnClick="btnSaveDocument_Click" runat="server"
                                    Width="119px" Font-Size="8pt" Text="Save Document" UseSubmitBehavior="False" OnClientClick="showPopup()" 
                                    CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" OnClick="btnCancel_Click" runat="server"
                                    Width="120px" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" CssClass="Button"  />
                            </ContentTemplate>
                             <Triggers>
        <asp:PostBackTrigger ControlID="btnSaveDocument"  />
    </Triggers> 
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
