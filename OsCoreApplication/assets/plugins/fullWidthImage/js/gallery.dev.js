var gallery = {
	window_width : 0,
	window_height : 0,
	is_menu : false,
	is_menu_opened : false,
	is_image_loaded : false,
	is_window_scrollable : false,
	map : {
		object : {},
		key : '',
		index : 0
	},
	config : {
		set_selector : 'div.js-gallery>div',
		set_svg_color : '#ffffff',
		set_background_opacity : 0.9,
		set_image_hover_transparency : true,
		set_image_hover_opacity : 0.8
	},
	construct : function(config)
	{
		this.init(config);
		this.setGallery();

		let group = [];

		if (config.hasOwnProperty('group'))
		{
			if (Array.isArray(config.group))
			{
				group = config.group;
			}
		}

		this.is_menu = (group.length > 1);

		let href = [],
			menu = [];

		for (let value of group)
		{
			this.map.object[value] = {};

			href.push(`a[rel=${value}]`);
			menu.push(`li[rel=${value}]`);
		}

		$('body').on('click', href.join(', ')+', '+menu.join(', '), function(event)
		{
			event.preventDefault();

			let is_menu = ($(this)[0].tagName.toLowerCase() === 'li');

			if (is_menu)
			{
				$('div#background, div#foreground').remove();
			}

			let rel_attr = $(this).attr('rel'),
				selector = `a[rel=${rel_attr}]`,
				href_attr,
				title_attr;

			gallery.map.key = rel_attr;
			gallery.map.index = is_menu ? 0 : $(this).parent().parent().find(selector).index($(this));

			if (!Object.keys(gallery.map.object[rel_attr]).length)
			{
				let counter = 0;

				$(selector).each(function()
				{
					href_attr = (typeof $(this).attr('href') === 'string' && $(this).attr('href').length) ? $(this).attr('href') : null;
					title_attr = (typeof $(this).attr('title') === 'string' && $(this).attr('title').length) ? $(this).attr('title') : null;

					gallery.map.object[rel_attr][counter] = {
						href : href_attr,
						title : title_attr
					};

					++counter;
				});
			}

			gallery.setWindowSize();
		});

		$('body').on('click', 'div#background>div.menu', function()
		{
			if ($('ul', 'div#foreground').css('display') === 'none')
			{
				gallery.is_menu_active = true;

				$('ul', 'div#foreground').fadeIn(600);
			}
			else
			{
				gallery.is_menu_active = false;

				$('ul', 'div#foreground').fadeOut(200);
			}
		});

		$('body').on('click', 'div#background>div.close', function()
		{
			$('div#background, div#foreground').remove();

			if (gallery.is_window_scrollable)
			{
				$('body').css('overflowY', 'scroll');
			}
		});

		if (this.config.set_image_hover_transparency)
		{
			if ($(`${this.config.set_selector}>a>img`).length)
			{
				$(`${this.config.set_selector}>a>img`).hover(
					function()
					{
						$(this).fadeTo(200, gallery.config.set_image_hover_opacity);
					},
					function()
					{
						$(this).fadeTo(200, 1);
					}
				);
			}
		}
	},
	init : function(config)
	{
		if (config.hasOwnProperty('set_selector'))
		{
			if (typeof config.set_selector === 'string')
			{
				if (new RegExp('^[A-Z]{1,3}[A-Z-_>\.]+$', 'gim').test(config.set_selector))
				{
					if ($(config.set_selector).length)
					{
						this.config.set_selector = config.set_selector;
					}
				}
			}
		}

		if (config.hasOwnProperty('set_svg_color'))
		{
			if (typeof config.set_svg_color === 'string')
			{
				if (new RegExp('^#([A-F0-9]{3}|[A-F0-9]{6})$', 'gim').test(config.set_svg_color))
				{
					this.config.set_svg_color = config.set_svg_color;
				}
			}
		}

		if (config.hasOwnProperty('set_background_opacity'))
		{
			if (typeof config.set_background_opacity === 'number')
			{
				if (config.set_background_opacity >= 0 && config.set_background_opacity <= 1)
				{
					this.config.set_background_opacity = config.set_background_opacity;
				}
			}
		}

		if (config.hasOwnProperty('set_image_hover_transparency'))
		{
			if (typeof config.set_image_hover_transparency === 'boolean')
			{
				this.config.set_image_hover_transparency = config.set_image_hover_transparency;
			}
		}

		if (config.hasOwnProperty('set_image_hover_opacity'))
		{
			if (typeof config.set_image_hover_opacity === 'number')
			{
				if (config.set_image_hover_opacity >= 0 && config.set_image_hover_opacity <= 1)
				{
					this.config.set_image_hover_opacity = config.set_image_hover_opacity;
				}
			}
		}
	},
	setGallery : function()
	{
		$(window).on('load', function()
		{
			gallery.is_window_scrollable = ($(document).height() > $(window).height());
		});

		let gallery_width = $(this.config.set_selector).parent().width(),
			divisor = 5,
			i = 0;

		$(this.config.set_selector).removeClass('eol');

		switch (true)
		{
			case gallery_width <= 767:
				divisor = 2;
			break;

			case gallery_width >= 768 && gallery_width <= 1023:
				divisor = 3;
			break;

			case gallery_width >= 1024 && gallery_width <= 1199:
				divisor = 4;
			break;
		}

		$(this.config.set_selector).each(function()
		{
			++i;

			if (i % divisor === 0)
			{
				$(this).addClass('eol');
			}
		});
	},
	setWindowSize : function()
	{
		if (this.is_window_scrollable)
		{
			$('body').css('overflowY', 'hidden');
		}

		this.window_width = $(window).width();
		this.window_height = $(window).height();

		this.setBackground();
		this.setForeground();
	},
	getDimension : function(width, height)
	{
		let x = (this.window_width > width) ? (this.window_width - width) / 2 : 0;
		let y = (this.window_height > height) ? (this.window_height - height) / 2 : 0;

		return [x, y];
	},
	setBackground : function()
	{
		let html = `<div id="background">
		<div class="content top"></div>
		<div class="menu"><img></div>
		<div class="close"><img></div>
		<div class="loading"><img></div>
		<div class="content bottom"></div>
		</div>`;

		$('body').append(html);

		$('div.menu>img').attr('src', this.setSVG(this.svg._menu_));
		$('div.close>img').attr('src', this.setSVG(this.svg._close_));
		$('div.loading>img').attr('src', this.setSVG(this.svg._loading_));

		if (this.is_menu)
		{
			$('div.menu').css('display', 'block');
		}

		$('div#background').css('opacity', this.config.set_background_opacity);

		let coordinates = this.getDimension(50, 50);

		$('div.loading', 'div#background').css({'top' : coordinates[1], 'left' : coordinates[0]});
	},
	setForeground : function()
	{
		this.is_image_loaded = false;

		let menu = '';

		if (this.is_menu)
		{
			for (let i in this.map.object)
			{
				menu += `<li rel="${i}">${i}</li>`;
			}
		}

		let html = `<div id="foreground">
		<div class="image"></div>
		<ul></ul>
		</div>`;

		$('body').append(html);
		$('div#foreground>ul').html(menu);

		let gallery_length = Object.keys(this.map.object[this.map.key]).length,
			image_number = this.map.index + 1;

		$('div.image', 'div#foreground').append(`<img src="${this.map.object[this.map.key][this.map.index].href}" onload="gallery.getImage();">`);
		$('div.content.top', 'div#background').text(this.map.object[this.map.key][this.map.index].title);
		$('div.content.bottom', 'div#background').text(`${image_number} / ${gallery_length}`);

		if (image_number !== 1 && gallery_length !== image_number)
		{
			this.setPreviousLink();
			this.setNextLink();
		}

		if (image_number === 1 && gallery_length !== 1)
		{
			this.setNextLink();
		}

		if (image_number === gallery_length && gallery_length !== 1)
		{
			this.setPreviousLink();
		}
	},
	getImage : function()
	{
		let img = new Image();

		img.src = $('div.image>img', 'div#foreground').attr('src');

		let is_resized = false,
			X = img.width,
			Y = img.height;

		if (Y > (this.window_height - 100))
		{
			is_resized = true;

			Y = this.window_height - 98;
			X = ((img.width / img.height) * Y).toFixed(0);
		}

		if (X > (this.window_width - 60))
		{
			is_resized = true;

			X = this.window_width - 62;
			Y = ((img.height / img.width) * X).toFixed(0);
		}

		if (is_resized)
		{
			$('div.image>img', 'div#foreground').attr('width', X);
			$('div.image>img', 'div#foreground').attr('height', Y);
		}

		let coordinates = this.getDimension(X, Y);

		$('div.loading', 'div#background').animate({width : X, height : Y, top : coordinates[1], left : coordinates[0]}, 200);
		$('div#foreground').css({'width' : X, 'height' : Y, 'top' : coordinates[1], 'left' : coordinates[0]}).fadeIn(600);

		let top = (Y - $('div#foreground>ul').height()) / 2,
			left = (X - $('div#foreground>ul').width()) / 2;

		$('div#foreground>ul').css({'top' : top, 'left' : left});

		this.is_menu_active = false;

		setTimeout(function()
		{
			gallery.is_image_loaded = true;
		}, 200);
	},
	setPreviousLink : function()
	{
		if (!$('div.previous', 'div#background').length)
		{
			$('div#background').prepend(`<div class="previous"><img></div>`);

			$('div.previous>img', 'div#background').attr('src', this.setSVG(this.svg._previous_));
		}

		$('div.previous', 'div#background').on('click', function()
		{
			if (!gallery.is_image_loaded)
			{
				return;
			}

			$('div.previous', 'div#background').remove();
			$('div#foreground').remove();

			--gallery.map.index;
			gallery.setForeground();
		});
	},
	setNextLink : function()
	{
		if (!$('div.next', 'div#background').length)
		{
			$('div#background').append(`<div class="next"><img></div>`);

			$('div.next>img', 'div#background').attr('src', this.setSVG(this.svg._next_));
		}

		$('div.next', 'div#background').on('click', function()
		{
			if (!gallery.is_image_loaded)
			{
				return;
			}

			$('div.next', 'div#background').remove();
			$('div#foreground').remove();

			++gallery.map.index;
			gallery.setForeground();
		});
	},
	setSVG : function(svg)
	{
		let svg_data = $.parseHTML(svg.trim());

		$(svg_data).attr('xmlns', 'http://www.w3.org/2000/svg').children().each(function()
		{
			$(this).attr({'stroke' : gallery.config.set_svg_color});
		});

		let blob = new Blob([$(svg_data)[0].outerHTML], {type : 'image/svg+xml'});

		return URL.createObjectURL(blob);
	},
	setOrientationChange : function()
	{
		$(window).on('resize', function()
		{
			gallery.setGallery();

			if ($('div.loading').length)
			{
				$('div#background, div#foreground').remove();

				gallery.setWindowSize();
			}
		});
	}(),
	keydown : function()
	{
		$(document).on('keydown', function(event)
		{
			switch (event.which)
			{
				case 27:
					if (gallery.is_menu_active)
					{
						$('div.menu', 'div#background').trigger('click');
					}
					else
					{
						$('div.close', 'div#background').trigger('click');
					}
				break;

				case 37:
					$('div.previous', 'div#background').trigger('click');
				break;

				case 39:
					$('div.next', 'div#background').trigger('click');
				break;

				case 13:
					return false;
			}
		});
	}(),
	svg : {
		_menu_ : `<svg width="100" height="100">
		<line x1="10" y1="25" x2="90" y2="25" stroke="#000000" stroke-width="6"></line>
		<line x1="10" y1="50" x2="90" y2="50" stroke="#000000" stroke-width="6"></line>
		<line x1="10" y1="75" x2="90" y2="75" stroke="#000000" stroke-width="6"></line>
		</svg>`,
		_close_ : `<svg width="100" height="100">
		<line x1="10" y1="10" x2="90" y2="90" stroke="#000000" stroke-width="6"></line>
		<line x1="10" y1="90" x2="90" y2="10" stroke="#000000" stroke-width="6"></line>
		</svg>`,
		_loading_ : `<svg width="100" height="100">
		<line x1="0" y1="25" x2="0" y2="25" stroke="#000000" stroke-width="6">
		<animate attributeName="x1" begin="0s" dur="1s" from="50" to="10" repeatCount="indefinite"></animate>
		<animate attributeName="x2" begin="0s" dur="1s" from="50" to="90" repeatCount="indefinite"></animate>
		</line>
		<line x1="0" y1="50" x2="0" y2="50" stroke="#000000" stroke-width="6">
		<animate attributeName="x1" begin="0.33s" dur="1s" from="50" to="10" repeatCount="indefinite"></animate>
		<animate attributeName="x2" begin="0.33s" dur="1s" from="50" to="90" repeatCount="indefinite"></animate>
		</line>
		<line x1="0" y1="75" x2="0" y2="75" stroke="#000000" stroke-width="6">
		<animate attributeName="x1" begin="0.66s" dur="1s" from="50" to="10" repeatCount="indefinite"></animate>
		<animate attributeName="x2" begin="0.66s" dur="1s" from="50" to="90" repeatCount="indefinite"></animate>
		</line>
		</svg>`,
		_next_ : `<svg width="60" height="100">
		<line x1="10" y1="10" x2="50" y2="52" stroke="#000000" stroke-width="6"></line>
		<line x1="10" y1="90" x2="50" y2="48" stroke="#000000" stroke-width="6"></line>
		</svg>`,
		_previous_ : `<svg width="60" height="100">
		<line x1="50" y1="10" x2="10" y2="52" stroke="#000000" stroke-width="6"></line>
		<line x1="50" y1="90" x2="10" y2="48" stroke="#000000" stroke-width="6"></line>
		</svg>`
	}
};