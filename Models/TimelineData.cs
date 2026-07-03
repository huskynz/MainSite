namespace _12.Models;

public static class TimelineData
{
    public static readonly IReadOnlyList<TimelineEvent> Entries = new[]
    {
        new TimelineEvent("2018", TimelineCategory.Infrastructure, new[]
        {
            "Deployed first Ubuntu 18.04 VM in Hyper-V",
        }),
        new TimelineEvent("2019", TimelineCategory.Career, new[]
        {
            "Continued experimenting with Hyper-V",
            "Moved to New Zealand",
            "Messed around with Virtualbox",
        }),
        new TimelineEvent("2020", TimelineCategory.Technology, new[]
        {
            "Started learning VMware Workstation Pro",
            "Deployed first Azure Web App",
            "Created www.husky.nz",
            "Created my Github account",
            "Started learning Linux",
            "Developed skills with Azure",
        }),
        new TimelineEvent("2021", TimelineCategory.Certification, new[]
        {
            "Earned AZ-900 Certification",
            "Learned more HTML and the web",
            "Deployed my first Active Directory domain on an Azure VM",
            "Configured Azure AD Connect to synchronize on-premises Active Directory with Azure Active Directory",
            "Remote Desktop Deployment with web access and MFA in Azure",
            "Contributed to development of a Azure Site to site vpn and Azure multi-network WAN",
            "Developed skills with the Microsoft 365 suite",
            "Developed skills with Intune and Windows Autopilot",
        }),
        new TimelineEvent("2022", TimelineCategory.Technology, new[]
        {
            "Expanded knowledge of Git and GitHub",
            "Studied .NET and .NET websites",
            "Began learning AWS",
            "Migrated AD Domain from Azure to AWS",
        }),
        new TimelineEvent("2023", TimelineCategory.Certification, new[]
        {
            "Migrated AD Domain from AWS to Azure",
            "Earned MS-900 Certification",
            "Gained work experience at Inde Technology (5 weeks)",
            "Began experimenting with Sophos Firewalls",
            "Started learning Node.js and TypeScript",
            "Began to learn more about Cloudflare",
        }),
        new TimelineEvent("2024", TimelineCategory.Project, new[]
        {
            "Used Azure migrate to move AD domain from AWS to Azure",
            "New Zealand Certificate in Information Technology Essentials (Level 4) at Ara",
            "Started streaming on Twitch",
            "Deployed a new version of www.husky.nz using TypeScript and Astro.js (This site)",
            "Had some fun developing https://legacy.husky.nz witch is a website built with HTML3.0 no CSS and no JavaScript",
            "Bought a HP DL380 G8 server",
            "ESXI + Vcenter 8",
            "Started getting the ground for hnz.li a url shortner built because I was annoyed",
        }),
        new TimelineEvent("2025", TimelineCategory.Infrastructure, new[]
        {
            "HP remote management",
            "Setup Microsoft Configuration Manager in my homelab",
            "Setup A SAN in my homelab to Learn and Understand network storage",
            "Setup Citrix Virtual Apps and Desktop and intrgrated with ESXi and Vcenter to auto provisioning and deployment of VDI vm's",
            "Deployed a more advanced Citrix lab with Netscaler, FAS and ADFS for SAML authentication",
            "Configured and deployed a Cisco ISR4451 router",
            "Expand hnz.li with redis caching to improve performance",
        }),
        new TimelineEvent("2026", TimelineCategory.Career, new[]
        {
            "Dive into Azure Dev ops pipelines",
            "Set Up a NSX enviroment with BGP advertisement to VYOS",
            "Set up Auto Scaling Azure Devops Pipeline agents on Vmware Tanzu",
            "Set up Vmware Tazu",
        }),
    };
}
