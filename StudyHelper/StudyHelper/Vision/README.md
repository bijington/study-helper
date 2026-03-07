# Azure OpenAI Account and API Key Setup Guide

This guide walks through the process of creating an Azure account, setting up Azure OpenAI Service, and generating API keys for use in the StudyHelper application.

## Prerequisites

- A valid email address
- A supported payment method (credit card) - **optional during free trial**
- A web browser with internet access
- An Azure subscription (can be created for free)

## Step 1: Create an Azure Account

1. Navigate to [Azure's website](https://azure.microsoft.com)
2. Click **Start free** or **Sign in** button
3. Enter your email address or existing Microsoft account credentials
4. Follow the authentication process (you may need to verify via phone or email)
5. Complete the sign-up form with your personal information
6. Provide a phone number for verification (will receive a code)
7. Verify your identity by entering the code received

## Step 2: Understanding Azure Free Trial

### Free Trial Benefits

- **$200 in Azure credits** (valid for 30 days)
- Access to most Azure services including Azure OpenAI
- No credit card required upfront (though you'll need to provide one for verification)
- Automatic upgrade occurs after trial ends or credits expire

### Important Notes

- Credits expire after 30 days
- Usage beyond the free tier limits will be charged if you don't disable services
- Free tier offers limited monthly usage (e.g., cognitive services have specific free tier limits per month)

## Step 3: Set Up Billing (Optional During Free Trial)

### During Free Trial
- You can use your $200 in credits without additional payment
- A credit card is required for identity verification but won't be charged during the trial

### Add Payment Method for Post-Trial Usage

1. Sign in to the [Azure Portal](https://portal.azure.com)
2. Click your user icon in the top-right corner
3. Select **Billing**
4. Go to **Subscriptions**
5. Click on your subscription name
6. Select **Payment methods** in the left sidebar
7. Click **Add payment method**
8. Enter your credit card details
9. Save your payment method

The trial will automatically convert to a pay-as-you-go subscription when credits expire or after 30 days.

## Step 4: Create an Azure OpenAI Resource

1. Sign in to the [Azure Portal](https://portal.azure.com)
2. Click **+ Create a resource** button (top-left)
3. Search for **"Azure OpenAI"** in the search bar
4. Click **Azure OpenAI** from the results
5. Click **Create**
6. Fill in the resource details:
   - **Subscription**: Select your subscription
   - **Resource group**: Create new or select existing (e.g., "StudyHelper-RG")
   - **Region**: Choose a region with OpenAI availability (e.g., East US, West Europe)
   - **Name**: Enter a unique name (e.g., "studyhelper-openai")
   - **Pricing tier**: Select "Standard S0"
7. Click **Review + create**
8. Review the details and click **Create**
9. Wait for deployment to complete (usually 1-2 minutes)

## Step 5: Deploy an Azure OpenAI Model

Models must be explicitly deployed in Azure OpenAI before use.

1. When deployment completes, click **Go to resource**
2. In the left sidebar, select **Model deployments** under "Management"
3. Click **Create new deployment** or **Deploy model**
4. In the dialog, select:
   - **Select a model**: Choose a model (e.g., "gpt-4" or "gpt-3.5-turbo")
   - **Deployment name**: Enter a unique name (e.g., "study-helper-gpt4")
   - **Model version**: Select the latest available version
5. Click **Create**
6. Note the deployment name - you'll need it in your application

## Step 6: Generate API Keys

### Option 1: Using the Azure Portal (Recommended)

1. In your Azure OpenAI resource, select **Keys and Endpoint** in the left sidebar
2. You'll see two keys: **key 1** and **key 2**
3. Click the copy icon next to either key to copy it
4. Store the key securely (see Step 7)

### Option 2: Using Access Keys

1. Select **Access control (IAM)** in the left sidebar
2. Click **+ Add** → **Add role assignment**
3. Assign roles as needed (typically "Cognitive Services OpenAI User" for application access)

**Important**: 

- You can view and regenerate keys anytime
- Keep both keys secure
- Regenerate keys if compromised
- The Endpoint URL is also visible on this page - you'll need both the key and endpoint

## Step 7: Secure Your API Credentials

### Never Commit to Version Control

- Don't add keys or endpoint URLs to Git repositories
- Add `.env` to `.gitignore`

### Store Credentials Securely

#### Option 1: Environment Variables

```
AZURE_OPENAI_API_KEY=your-api-key-here
AZURE_OPENAI_ENDPOINT=https://your-resource-name.openai.azure.com/
```

#### Option 2: .env File (Development Only)

1. Create a `.env` file in your project root:
```
AZURE_OPENAI_API_KEY=your-api-key-here
AZURE_OPENAI_ENDPOINT=https://your-resource-name.openai.azure.com/
AZURE_OPENAI_DEPLOYMENT_NAME=your-deployment-name
```

2. Add `.env` to `.gitignore`:

```
.env
.env.local
*.key
```

#### Option 3: Azure Key Vault (Production)

For production applications, store secrets in Azure Key Vault:

1. Create an Azure Key Vault resource
2. Store your API keys as secrets
3. Reference them from your application using managed identities

## Step 8: Using Your Credentials in the Application

When making API calls to Azure OpenAI:

- Use the endpoint URL from "Keys and Endpoint" page
- Use one of your API keys
- Specify the deployment name you created in Step 5

Example configuration:

```
Endpoint: https://studyhelper-openai.openai.azure.com/
API Key: [your-key]
Deployment Name: study-helper-gpt4
```

## Step 9: Monitor Your Usage and Costs

### View Usage

1. In your Azure OpenAI resource, select **Metrics** in the left sidebar
2. View real-time usage metrics
3. Monitor **Prompt tokens** and **Completion tokens**

### Set Up Alerts

1. Select **Alerts** in the left sidebar
2. Click **+ Create alert rule**
3. Set conditions for notifications (e.g., monthly cost threshold)
4. Configure notification actions (email, SMS, etc.)

### During Free Trial

- Monitor your remaining $200 credits on the [Azure Cost Management page](https://portal.azure.com/#view/Microsoft_Azure_CostManagement/Menu)
- See daily consumption estimates

## Differences: OpenAI API vs Azure OpenAI

| Feature | OpenAI API | Azure OpenAI |
|---------|-----------|--------------|
| **Authentication** | API Key only | API Key + Endpoint URL |
| **Pricing** | Pay-as-you-go | Pay-as-you-go (via Azure billing) |
| **Deployment** | Managed by OpenAI | Self-managed deployments |
| **SLA** | Standard | Enterprise SLA available |
| **Regional Availability** | Global | Select Azure regions |
| **Integration** | Direct API calls | Azure ecosystem integration |

## Troubleshooting

### API Key Not Working

- Verify the key hasn't been regenerated
- Ensure you're using the correct endpoint URL
- Check that the resource is in the same region as your deployment
- Confirm the deployment exists and is active

### Deployment Not Found

- Verify the deployment name matches exactly (case-sensitive)
- Ensure the deployment has been created
- Check that the deployment is in the same resource

### Rate Limiting

- Azure OpenAI has rate limits based on your tier
- Check Token Per Minute (TPM) limits for your deployment
- Scale up the deployment capacity if needed

### Free Trial Expired

- Convert to pay-as-you-go subscription
- Add a payment method on the Subscriptions page
- Existing resources will continue to work with billing

## Support and Resources

- [Azure OpenAI Documentation](https://learn.microsoft.com/en-us/azure/ai-services/openai/)
- [Azure OpenAI Quickstart](https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart)
- [Azure Cost Management](https://portal.azure.com/#view/Microsoft_Azure_CostManagement/Menu)
- [Azure Support](https://azure.microsoft.com/en-us/support/)

## Security Best Practices

✓ Rotate API keys regularly  
✓ Use Azure Key Vault for production  
✓ Implement resource access controls (IAM)  
✓ Monitor API access with Azure logging  
✓ Use managed identities instead of keys when possible  
✓ Enable Azure Firewall rules to restrict access by IP  
✓ Never share credentials in emails or chat  
✓ Use separate keys for development and production  
✓ Delete unused resources to reduce costs  
✓ Enable Azure Monitor for audit logging  
