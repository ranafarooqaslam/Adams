<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmVehicle.aspx.cs" Inherits="Forms_frmVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">    
    <div id="right_data">
        <script type="text/javascript">
        $("input:text").keypress(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });
    </script>
        <asp:UpdatePanel ID="pnl_head" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDistributor" runat="server" Width="180px" CssClass="DropList"
                                AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label1" runat="server" Text="Vehicle No" CssClass="label"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVehicleno" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 10px;">
                        </td>
                        <td>
                            <strong>
                                <asp:Label ID="Label2" runat="server" Text="Make"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMake" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label3" runat="server" Text="Model"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModel" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 10px;">
                        </td>
                        <td>
                            <strong>
                                <asp:Label ID="Label4" runat="server" Text="Engine No"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEngine" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="Label5" runat="server" Text="Chassis No"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChassisno" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 10px;">
                        </td>
                        <td>
                            <strong>
                                <asp:Label ID="Label6" runat="server" Text="Assign To" Visible="false" ></asp:Label></strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="DrpAssignTo" runat="server" Visible="false" 
                                Width="180px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     
                    <tr>
                        <td>
                           <strong> <asp:CheckBox ID="chbIs_Active" Text="  Is Active" runat="server" Enabled="false" Checked="true"/></strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" OnClick="btnSave_Click"
                                Width="80px" />
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 10px;">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                        
                    <asp:Panel ID="pnl_grd" runat="server" Width="650px" Height="250px" BorderWidth="1px" BorderColor="Silver" ScrollBars="Vertical">
                        <asp:GridView ID="grd_vehicle" runat="server" ForeColor="SteelBlue" CssClass="gridRow2" 
                             HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White"
                            OnRowCommand="grd_vehicle_RowCommand">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" CssClass="tblhead" />
                            <Columns>
                                <asp:BoundField DataField="VEHICLE_ID" HeaderText="VEHICLE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VEHICLE_NO" HeaderText="Vehicle No">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        Width="75px" ></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MAKE" HeaderText="Make">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MODEL" HeaderText="Model">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ENGINE_NO" HeaderText="Engine No">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHASSIS_NO" HeaderText="Chassis No">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="Assign To">
                                       <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="IS_ACTIVE" HeaderText="Is Active">
                                    <ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                                        Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
                                     <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ASSIGN_TO" HeaderText="ASSIGN_ID">
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
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </td>
                    </tr>
                </table>
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
