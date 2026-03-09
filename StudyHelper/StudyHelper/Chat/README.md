# OpenAI Account and API Key Setup Guide

This guide walks through the process of creating an OpenAI account and generating an API key for use in the StudyHelper application.

## Prerequisites

- A valid email address
- A web browser with internet access
- A supported payment method (credit card) — **optional during free trial, required after trial credits expire**

## Step 1: Create an OpenAI Account

1. Navigate to [OpenAI's website](https://openai.com)
2. Click the **Sign Up** button in the top-right corner
3. Enter your email address
4. Click **Continue**
5. Create a strong password and click **Continue**
6. Verify your email address by clicking the link sent to your inbox
7. Fill in your profile information (first name, last name)
8. Complete any additional verification steps if required (phone number verification)

## Step 2: Set Up Billing (Optional During Free Trial)

### Free Trial Phase

When you first create your account, OpenAI provides:

- **$5 in free API credits** (typically valid for 3 months)
- No payment method required to get started with development and testing
- Limited rate limits compared to paid accounts

You can skip billing during this phase and proceed directly to generating an API key.

### Add Billing Later (Required After Free Trial Expires)
When your free credits expire or to continue after the trial period:

1. After creating your account, you'll be directed to the dashboard
2. Click on your account icon in the top-right corner
3. Select **Billing overview** from the dropdown menu
4. Click **Set up paid account** or **Add to paid account**
5. Enter your payment method details (credit card)
6. Complete the billing address information
7. Confirm your payment method

## Step 3: Generate an API Key

1. On the OpenAI dashboard, click your account icon in the top-right corner
2. Select **API keys** from the dropdown menu (or navigate to [API keys page](https://platform.openai.com/api-keys))
3. Click the **Create new secret key** button
4. Choose a name for your key (e.g., "StudyHelper App")
5. Click **Create secret key**
6. **IMPORTANT**: Copy your API key immediately and store it securely. You will not be able to view it again.

**Note**: You can generate an API key before adding billing. The key will work during your free trial period.

## Step 4: Secure Your API Key

1. **Never commit your API key to version control** (Git, GitHub, etc.)
2. Store your API key in one of these secure locations:
   - A `appsettings.Development.json` file (add to `.gitignore`)
   - Environment variables on your development machine
   - A `.env` file (add `.env` to `.gitignore`)
   - A secure secrets manager

### Option 1: Using `appsettings.Development.json` (Recommended for .NET)

1. Create or edit `appsettings.Development.json` in the project root:
```json
{
  "OpenAI": {
    "ApiKey": "sk-your-api-key-here"
  }
}
```

2. Ensure `appsettings.Development.json` is in your `.gitignore` file
3. Reference the key in your application configuration

### Option 2: Using `.env` file setup

```
OPENAI_API_KEY=sk-your-api-key-here
```

### Add to `.gitignore`

Ensure both configuration files are ignored:
```
appsettings.Development.json
.env
.env.local
*.key
```

## Step 5: Using Your API Key in the Application

Once you have your API key:

1. Set the environment variable on your deployment machine or development environment
2. Reference it in your application code using environment variables
3. Never hardcode the API key directly in your source code

## Step 6: Monitor Your Usage

1. Return to the **Billing overview** page regularly to check your credit balance (or costs if on a paid plan)
2. Check your **Usage** page to monitor API call consumption
3. **For Free Trial**: Track your remaining free credits
4. **For Paid Plans**: Set usage limits to prevent unexpected charges:
   - Go to **Billing** → **Usage limits**
   - Set a **Hard limit** to control maximum monthly spending
   - Set a **Soft limit** to receive alerts when approaching your budget

## Troubleshooting

### Account Locked or Unusual Activity

- Check your email for security alerts
- Verify your account hasn't been compromised
- Reset your password if needed

### API Key Not Working

- Ensure the key is still active (not deleted)
- Verify there are no typos in the key
- Check that your account has available credits
- Ensure billing information is current and valid

### Rate Limiting

- OpenAI implements rate limits based on your account tier
- Monitor your usage and implement exponential backoff in your application
- Consider upgrading your account tier for higher limits

## Support and Resources

- [OpenAI API Documentation](https://platform.openai.com/docs)
- [OpenAI Help Center](https://help.openai.com)
- [OpenAI Community Forum](https://community.openai.com)

## Security Best Practices

✓ Rotate your API keys regularly  
✓ Use environment variables or secrets managers  
✓ Never share your API key publicly  
✓ Monitor your account for unauthorized access  
✓ Delete unused API keys  
✓ Use IP whitelisting if available on your account tier  
