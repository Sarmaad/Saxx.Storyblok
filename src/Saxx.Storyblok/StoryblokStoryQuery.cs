﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Saxx.Storyblok
{
    public class StoryblokStoryQuery
    {
        private readonly StoryblokClient _client;

        private CultureInfo _culture;
        private string _slug = "";

        public StoryblokStoryQuery(StoryblokClient client)
        {
            _client = client;
        }

        public StoryblokStoryQuery WithSlug(string slug)
        {
            if (slug != null)
            {
                _slug = slug;
            }

            return this;
        }

        public StoryblokStoryQuery WithCulture(CultureInfo culture)
        {
            if (culture != null)
            {
                _culture = culture;
            }

            return this;
        }


        public async Task<StoryblokStory<T>> Load<T>() where T : StoryblokComponent
        {
            return await _client.LoadStory<T>(_culture, _slug);
        }

        // ReSharper disable once UnusedMember.Global
        public async Task<StoryblokStory> Load()
        {
            return await _client.LoadStory(_culture, _slug);
        }
    }
}