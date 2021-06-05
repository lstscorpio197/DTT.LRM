using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using DTT.LRM.EntityFramework;

namespace DTT.LRM.Migrations.SeedData
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages { get; private set; }

        private readonly LRMDbContext _context;

        static DefaultLanguagesCreator()
        {
            InitialLanguages = new List<ApplicationLanguage>
            {
                new ApplicationLanguage(null, "en", "English", "famfamfam-flags gb",true),
                new ApplicationLanguage(null, "tr", "Türkçe", "famfamfam-flags tr",true),
                new ApplicationLanguage(null, "zh-CN", "简体中文", "famfamfam-flags cn",true),
                new ApplicationLanguage(null, "pt-BR", "Português-BR", "famfamfam-flags br",true),
                new ApplicationLanguage(null, "es", "Español", "famfamfam-flags es",true),
                new ApplicationLanguage(null, "fr", "Français", "famfamfam-flags fr",true),
                new ApplicationLanguage(null, "it", "Italiano", "famfamfam-flags it",true),
                new ApplicationLanguage(null, "ja", "日本語", "famfamfam-flags jp",true),
                new ApplicationLanguage(null, "nl-NL", "Nederlands", "famfamfam-flags nl",true),
                new ApplicationLanguage(null, "lt", "Lietuvos", "famfamfam-flags lt",true),
                new ApplicationLanguage(null, "vi", "Tiếng Việt", "famfamfam-flags vi",false)
            };
        }

        public DefaultLanguagesCreator(LRMDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}
