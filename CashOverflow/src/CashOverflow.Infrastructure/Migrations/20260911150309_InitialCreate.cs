using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashOverflow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    avatar_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    user_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "bit", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    security_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "bit", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "bit", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "bit", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DemoRequests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    company_email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    company_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    submitted_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_demo_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FxRates",
                columns: table => new
                {
                    rate_date = table.Column<DateOnly>(type: "date", nullable: false),
                    base_currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    quote_currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    rate = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fx_rates", x => new { x.rate_date, x.base_currency, x.quote_currency });
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    provider_key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    provider_display_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    login_provider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    claim_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    claim_value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    legal_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tax_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sector = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    size_band = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    governorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    employee_count = table.Column<int>(type: "int", nullable: false),
                    founded_date = table.Column<DateOnly>(type: "date", nullable: false),
                    base_currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    opening_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    min_cash_buffer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    credit_line_limit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    credit_line_drawn = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    is_demo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.company_id);
                    table.ForeignKey(
                        name: "fk_companies_organizations_company_id",
                        column: x => x.company_id,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "invitations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organization_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    accepted_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invitations", x => x.id);
                    table.ForeignKey(
                        name: "fk_invitations_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationMembers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    application_user_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    organization_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_organization_members_application_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "ApplicationUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_organization_members_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organization_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    subscription_plan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscriptions_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    account_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    iban = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    opening_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    opened_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_accounts", x => x.account_id);
                    table.ForeignKey(
                        name: "fk_bank_accounts_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CounterParties",
                columns: table => new
                {
                    counterparty_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tax_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    sector = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    size_band = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    governorate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_government = table.Column<bool>(type: "bit", nullable: false),
                    default_payment_terms_days = table.Column<int>(type: "int", nullable: false),
                    risk_segment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    relationship_start = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_counter_parties", x => x.counterparty_id);
                    table.ForeignKey(
                        name: "fk_counter_parties_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecurringObligations",
                columns: table => new
                {
                    obligation_id = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 64, nullable: false),
                    kind = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    day_of_month = table.Column<int>(type: "int", nullable: false),
                    business_day_rule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    variability_pct = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recurring_obligations", x => x.obligation_id);
                    table.ForeignKey(
                        name: "fk_recurring_obligations_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subscription_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organization_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    method = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    provider_payment_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    paid_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_subscriptions_subscription_id",
                        column: x => x.subscription_id,
                        principalTable: "Subscriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankTransactions",
                columns: table => new
                {
                    transaction_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    account_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    booking_date = table.Column<DateOnly>(type: "date", nullable: false),
                    value_date = table.Column<DateOnly>(type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    credit_debit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    bank_tx_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    end_to_end_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    remittance_info = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    counterparty_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    running_balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_transactions", x => x.transaction_id);
                    table.ForeignKey(
                        name: "fk_bank_transactions_bank_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "BankAccounts",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "fk_bank_transactions_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    internal_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    counterparty_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    direction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    document_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    issue_date = table.Column<DateOnly>(type: "date", nullable: false),
                    due_date = table.Column<DateOnly>(type: "date", nullable: false),
                    payment_terms_days = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    fx_rate_to_base = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    net_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    discount_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    vat_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    retention_pct = table.Column<decimal>(type: "decimal(9,4)", precision: 9, scale: 4, nullable: false),
                    retention_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    reference_uuid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoices", x => x.uuid);
                    table.ForeignKey(
                        name: "fk_invoices_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "fk_invoices_counter_parties_counterparty_id",
                        column: x => x.counterparty_id,
                        principalTable: "CounterParties",
                        principalColumn: "counterparty_id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    line_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    invoice_uuid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    line_number = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    item_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    unit_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    discount_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    net_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    vat_rate = table.Column<decimal>(type: "decimal(9,4)", precision: 9, scale: 4, nullable: false),
                    vat_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoice_lines", x => x.line_id);
                    table.ForeignKey(
                        name: "fk_invoice_lines_invoices_invoice_uuid",
                        column: x => x.invoice_uuid,
                        principalTable: "Invoices",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    settlement_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    invoice_uuid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    company_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    paid_date = table.Column<DateOnly>(type: "date", nullable: false),
                    paid_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sequence_no = table.Column<int>(type: "int", nullable: false),
                    is_final = table.Column<bool>(type: "bit", nullable: false),
                    bank_transaction_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_settlements", x => x.settlement_id);
                    table.ForeignKey(
                        name: "fk_settlements_bank_transactions_bank_transaction_id",
                        column: x => x.bank_transaction_id,
                        principalTable: "BankTransactions",
                        principalColumn: "transaction_id");
                    table.ForeignKey(
                        name: "fk_settlements_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "company_id");
                    table.ForeignKey(
                        name: "fk_settlements_invoices_invoice_uuid",
                        column: x => x.invoice_uuid,
                        principalTable: "Invoices",
                        principalColumn: "uuid");
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ApplicationUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ApplicationUsers",
                column: "normalized_user_name",
                unique: true,
                filter: "[normalized_user_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true,
                filter: "[normalized_name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_bank_accounts_company_id",
                table: "BankAccounts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_bank_accounts_iban",
                table: "BankAccounts",
                column: "iban",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_bank_transactions_account_id",
                table: "BankTransactions",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "ix_bank_transactions_booking_date",
                table: "BankTransactions",
                column: "booking_date");

            migrationBuilder.CreateIndex(
                name: "ix_bank_transactions_company_id",
                table: "BankTransactions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_companies_tax_id",
                table: "Companies",
                column: "tax_id");

            migrationBuilder.CreateIndex(
                name: "ix_counter_parties_company_id",
                table: "CounterParties",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_demo_requests_company_email",
                table: "DemoRequests",
                column: "company_email");

            migrationBuilder.CreateIndex(
                name: "ix_demo_requests_submitted_at",
                table: "DemoRequests",
                column: "submitted_at");

            migrationBuilder.CreateIndex(
                name: "ix_invitations_organization_id",
                table: "invitations",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_invoice_lines_invoice_uuid",
                table: "InvoiceLines",
                column: "invoice_uuid");

            migrationBuilder.CreateIndex(
                name: "ix_invoice_lines_invoice_uuid_line_number",
                table: "InvoiceLines",
                columns: new[] { "invoice_uuid", "line_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_invoices_company_id",
                table: "Invoices",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_invoices_counterparty_id",
                table: "Invoices",
                column: "counterparty_id");

            migrationBuilder.CreateIndex(
                name: "ix_invoices_due_date",
                table: "Invoices",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "ix_invoices_status",
                table: "Invoices",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_organization_members_application_user_id_organization_id",
                table: "OrganizationMembers",
                columns: new[] { "application_user_id", "organization_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_organization_members_organization_id",
                table: "OrganizationMembers",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_organization_id",
                table: "Payments",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_provider_payment_id",
                table: "Payments",
                column: "provider_payment_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_subscription_id",
                table: "Payments",
                column: "subscription_id");

            migrationBuilder.CreateIndex(
                name: "ix_recurring_obligations_company_id",
                table: "RecurringObligations",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_settlements_bank_transaction_id",
                table: "Settlements",
                column: "bank_transaction_id");

            migrationBuilder.CreateIndex(
                name: "ix_settlements_company_id",
                table: "Settlements",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_settlements_invoice_uuid",
                table: "Settlements",
                column: "invoice_uuid");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_organization_id",
                table: "Subscriptions",
                column: "organization_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DemoRequests");

            migrationBuilder.DropTable(
                name: "FxRates");

            migrationBuilder.DropTable(
                name: "invitations");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "OrganizationMembers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RecurringObligations");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "BankTransactions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CounterParties");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
