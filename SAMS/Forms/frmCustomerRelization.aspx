<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerRelization.aspx.cs" Inherits="Forms_frmCustomerRelization"
    Title="SAMS :: Bank Transaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }

        }
        function pageLoad() {
            $("select").searchable();
            $('#<%=GrdOrder.ClientID %>').tablesorter(
	     {
	         headers: {
	             3: {
	                 sorter: false
	             },
	             6: {
	                 sorter: false
	             },
	             10: {
	                 sorter: false
	             },
	             11: {
	                 sorter: false
	             },
	             12: {
	                 sorter: false
	             },
	             13: {
	                 sorter: false
	             }
	         }
	     }
	     );
            $('#<%=gvSaleForceCash.ClientID %>').tablesorter(
	     {
	         headers: {
	             0: {
	                 sorter: false
	             },
	             1: {
	                 sorter: false
	             },
	             2: {
	                 sorter: false
	             },
	             3: {
	                 sorter: false
	             },
	             4: {
	                 sorter: false
	             },
	             5: {
	                 sorter: false
	             },
	             7: {
	                 sorter: false
	             },
	             8: {
	                 sorter: false
	             }
	         }
	     }
	     );
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lblfromLocation" runat="server" Text="Location"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px"
                                                OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Text="H.O. Account Code"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:45%">
                                            <asp:DropDownList ID="ddlHeadOffice" runat="server" Width="200px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="Label8" runat="server" Text="Principal"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px"
                                                OnSelectedIndexChanged="DrpPrincipal_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="Label10" runat="server" Text="Sale Force"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:45%">
                                            <asp:DropDownList ID="DrpDeliveryMan" runat="server" Width="200px"
                                                OnSelectedIndexChanged="DrpDeliveryMan_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="Label1" runat="server" Text="Account Type"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpAccountType" runat="server" Width="200px" 
                                                OnSelectedIndexChanged="DrpAccountType_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="19">Cash Realization</asp:ListItem>
                                                <asp:ListItem Value="21">Cash Advance</asp:ListItem>
                                                <asp:ListItem Value="22">Bank Deposit (Branch)</asp:ListItem>
                                                <asp:ListItem Value="23">Income Tax</asp:ListItem>
                                                <asp:ListItem Value="28">Credit Transfer Out</asp:ListItem>
                                                <asp:ListItem Value="29">Advance Return</asp:ListItem>
                                                <asp:ListItem Value="222">Bank Deposit (DM)</asp:ListItem>
                                                <asp:ListItem>Cash From DM</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Text="Route"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:45%">
                                            <asp:DropDownList ID="DrpRoute" runat="server" Width="200px" 
                                                OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Text="Account"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpAccountDetail" runat="server" Width="200px" 
                                                OnSelectedIndexChanged="DrpAccountDetail_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:15%">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Customer"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:45%">
                                            <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px"
                                                OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                            <td style="width:10%" valign="top">                                                
                                                <strong>
                                                <asp:Label ID="Label5" runat="server" Text="Cheque No"></asp:Label>
                                            </strong>                                                    
                                            </td>
                                            <td style="width:30%" valign="top">
                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="194px"></asp:TextBox>                                                
                                            </td>
                                            <td valign="middle" align="left" colspan="2" rowspan="8" style="width:50%">
                                                <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" BorderColor="Silver"
                                                    BorderStyle="Solid" BorderWidth="1px" Width="60%">
                                                    <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                        Width="100%" OnRowDeleting="GrdOrder_RowDeleting" DataKeyNames="SALE_INVOICE_ID">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChbIsAssigned" runat="server" Width="14px" />
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MANUAL_INVOICE_ID" HeaderText="Invoice No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CURRENT_CREDIT_AMOUNT" HeaderText="Credit Amount" DataFormatString="{0:F2}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Text="Slip No"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtSlipNo" runat="server" Width="194px"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            
                                        </td>
                                        <td style="width:45%">                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="74px" Text="Amount"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtAmount" runat="server" Width="194px"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            
                                        </td>
                                        <td style="width:45%">                                         
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Text="Remarks"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="194px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        </td>
                                        <td style="width:15%">
                                            
                                        </td>
                                        <td style="width:45%">                                         
                                        </td>
                                    </tr>                                    
                                </table>
                                <table>
                                    <tbody>  
                                        <tr>
                                            <td style="height: 36px" align="left">
                                                <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="102px"
                                                    Font-Size="8pt" Text="Save" CssClass="Button" />
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                                    Text="Cancel" CssClass="Button" />
                                                <div style="z-index: 101; left: 295px; width: 90px; position: absolute; top: 250px;
                                                    height: 90px">
                                                    <asp:Panel ID="Panel21" runat="server">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                            <ProgressTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                                    Width="23px" />
                                                                Wait Update
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 100px; height: 36px" valign="middle" align="left">
                                            </td>
                                            <td style="width: 201px; height: 36px" valign="middle" align="left">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                &nbsp;<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom" ValidChars="0123456789." TargetControlID="txtAmount">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DrpRoute" EventName="SelectedIndexChanged">
                                </asp:AsyncPostBackTrigger>
                                <asp:PostBackTrigger ControlID="btnSave"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 650px;">
                        <tbody>
                            <tr>
                                <td style="height: 20px" align="left" colspan="5">
                                    <asp:Panel ID="Panel12" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="850px" Height="200px" ScrollBars="Vertical">
                                        <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
                                            BorderColor="White" OnRowDeleting="GrdOrder_RowDeleting" HorizontalAlign="Center"
                                            BackColor="White" AutoGenerateColumns="False">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous"></PagerSettings>
                                            <Columns>
                                                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="voucher_type_id" HeaderText="voucher_type_id">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_HIE_NAME" HeaderText="Principal">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Manual_Document_no" HeaderText="Document No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Ledger_date" HeaderText="Ledger Date">
                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Voucher_no" HeaderText="Voucher No">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Balance" DataFormatString="{0:F2}" HeaderText="Amount">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Document_no" HeaderText="Document_no">
                                                         <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                                </asp:TemplateField>                                                
                                            </Columns>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                            </HeaderStyle>
                                        </asp:GridView>
                                        <asp:GridView ID="gvSaleForceCash" runat="server" Visible="False" Width="728px" ForeColor="SteelBlue"
                                            CssClass="tablesorter" BorderColor="White" OnRowDeleting="gvSaleForceCash_RowDeleting"
                                            HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False" OnRowCommand="gvSaleForceCash_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="SALE_FORCE_CASH_ID" HeaderText="SALE_FORCE_CASH_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRINCIPAL" HeaderText="PRINCIPAL">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DELIVERYMAN_ID" HeaderText="DELIVERYMAN_ID">
                                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DELIVERYMAN" HeaderText="DELIVERY MAN">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="DOCUMENT DATE">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT">
                                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
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
                                                        <asp:LinkButton ID="btnDeleteSaleForceCash" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                           <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                                            </HeaderStyle>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfSALE_FORCE_CASH_ID" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hfPRINCIPAL_ID" runat="server" __designer:wfdid="w2"></asp:HiddenField>
                                        <asp:HiddenField ID="hfDELIVERYMAN_ID" runat="server" __designer:wfdid="w3"></asp:HiddenField>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
