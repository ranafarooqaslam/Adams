<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRollBackForm.aspx.cs" Inherits="Forms_frmRollBackForm" Title="SAMS :: Rollback Transaction" %>
 <%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
 <script type="text/javascript">
     function pageLoad() {
         $("select").searchable();
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
            <table width="100%">
                <tr>
                    <td>
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
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="104px" Text="Transaction Type" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpDocumentType" runat="server" Width="200px" CssClass="DropList">
                                                <asp:ListItem Value="0">Order Entry</asp:ListItem>
                                                <asp:ListItem Value="1">Sale Invoice</asp:ListItem>
                                                <asp:ListItem Value="2">Sale Return</asp:ListItem>
                                                <asp:ListItem Value="3">Realized Cheque</asp:ListItem>
                                                <asp:ListItem Value="4">Goods Receipt Note</asp:ListItem>
                                            </asp:DropDownList>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
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
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 25px">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="94px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="DrpOrderBooker" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Legend" Width="94px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="DrpLenged" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    
                                     <tr>
                                        <td> &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td  colspan="2">
                                           &nbsp;
                                            <asp:Button ID="btnGetOrder" OnClick="btnGetOrder_Click" runat="server" Width="85px"
                                                Font-Size="8pt" Text="Get Data" CssClass="Button" />
                                     &nbsp;
                                            <asp:Button ID="btnPost" runat="server" Font-Size="8pt" Text="Rollback" Width="100px"
                                                OnClick="btnPost_Click" CssClass="Button" OnClientClick="showPopup()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                          
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                               <%-- <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                    width: 650px; border-bottom: silver thin inset">--%>
                                       <table style="width: 650px;">
                                    <tbody>
                                        <tr>
                                            <td style="height: 21px" align="left" colspan="5">
                                                <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px" Width="750px" Height="250px" ScrollBars="Vertical">
                                                    <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        DataKeyNames="Document_ID" HorizontalAlign="Center" BorderColor="White" BackColor="White"
                                                        AutoGenerateColumns="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Document Id">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Document Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Gross Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DISCOUNT_AMOUNT" DataFormatString="{0:F2}" HeaderText="Discount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SCHEME_AMOUNT" DataFormatString="{0:F2}" HeaderText="Scheme">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="GST_AMOUNT" DataFormatString="{0:F2}" HeaderText="GST Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TOTAL_NET_AMOUNT" DataFormatString="{0:F2}" HeaderText="Net Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="GrdCheque" runat="server" Visible="False" Width="700px" ForeColor="SteelBlue"
                                                        CssClass="gridRow2" HorizontalAlign="Center" BorderColor="White" BackColor="White"
                                                        AutoGenerateColumns="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous"></PagerSettings>
                                                        <Columns>
                                                            <asp:BoundField DataField="CHEQUE_PROCESS_ID" HeaderText="CHEQUE_PROCESS_ID">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
                                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CUSTOMER_CODE2" HeaderText="Code">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER" HeaderText="Name">
                                                                <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Voucher_No" HeaderText="Voucher No" Visible="False">
                                                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No">
                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_DATE" HeaderText="Cheque Date">
                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_AMOUNT" DataFormatString="{0:F2}" HeaderText="Cheque Amount">
                                                                <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                                </ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                                        </HeaderStyle>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GrdPurchase" runat="server" Width="700px" ForeColor="SteelBlue" CssClass="gridRow2"
                                                        DataKeyNames="Document_ID" HorizontalAlign="Center" BorderColor="White" BackColor="White"
                                                        AutoGenerateColumns="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                           
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbInvoice" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:TemplateField>
                                                          <asp:BoundField DataField="Document_Id" HeaderText="Document Id">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Document Date"  DataFormatString="{0:dd-MMM-yyyy}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                             <asp:BoundField DataField="TOTAL_QTY"  HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TOTAL_AMOUNT" DataFormatString="{0:F2}" HeaderText="Total Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                           
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
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
        </div>
    </div>
</asp:Content>
