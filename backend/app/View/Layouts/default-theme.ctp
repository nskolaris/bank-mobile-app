<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->

    <!-- START @HEAD -->
    <head>
		<!-- START @META SECTION -->
		<?php echo $this->Html->charset(); ?>
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<?php $theme = 'assets/blankon-fullpack-admin-theme'; ?>
        <title><?php echo $title_for_layout; ?></title>
        <!--/ END META SECTION -->

        <!-- START @FAVICONS -->
        <link href="<?php echo $this->webroot . $theme ?>/global/img/ico/apple-touch-icon-144x144-precomposed.png" rel="apple-touch-icon-precomposed" sizes="144x144">
        <link href="<?php echo $this->webroot . $theme ?>/global/img/ico/apple-touch-icon-114x114-precomposed.png" rel="apple-touch-icon-precomposed" sizes="114x114">
        <link href="<?php echo $this->webroot . $theme ?>/global/img/ico/apple-touch-icon-72x72-precomposed.png" rel="apple-touch-icon-precomposed" sizes="72x72">
        <link href="<?php echo $this->webroot . $theme ?>/global/img/ico/apple-touch-icon-57x57-precomposed.png" rel="apple-touch-icon-precomposed">
        <link href="<?php echo $this->webroot . $theme ?>/global/img/ico/apple-touch-icon.png" rel="shortcut icon">
        <!--/ END FAVICONS -->

        <!-- START @FONT STYLES -->
        <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700" rel="stylesheet">
        <!--/ END FONT STYLES -->

        <!-- START @GLOBAL MANDATORY STYLES -->
        <link href="<?php echo $this->webroot . $theme ?>/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
        <!--/ END GLOBAL MANDATORY STYLES -->

        <!-- START @PAGE LEVEL STYLES -->
        <link href="<?php echo $this->webroot . $theme ?>/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/global/plugins/animate/animate.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/global/plugins/dropzone/css/dropzone.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/global/plugins/gritter/css/jquery.gritter.css" rel="stylesheet">
        <!--/ END PAGE LEVEL STYLES -->

        <!-- START @THEME STYLES -->
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/reset.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/layout.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/components.min.css" rel="stylesheet">
        <!--<link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/plugins.min.css" rel="stylesheet">-->
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/themes/minified/default.theme.min.css" rel="stylesheet" id="theme">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/custom.css" rel="stylesheet">
        <!--/ END THEME STYLES -->

		<link href="<?php echo $this->webroot ?>css/datepicker.css" rel="stylesheet">
		
        <!-- START @IE SUPPORT -->
        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
        <!--/ END IE SUPPORT -->
    </head>
    <!--/ END HEAD -->

	<script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery/jquery.min.js"></script>
	
	
    <body class="page-session page-sound page-header-fixed page-sidebar-fixed">

        <!-- START @LOADING ANIMATION -->
        <div id="loading">
            <div class="loading-inner">
                <img src="<?php echo $this->webroot . $theme ?>/global/img/loader/flat/3.gif" alt="..."/>
            </div>
        </div><!-- /#loading -->
        <!--/ END LOADING ANIMATION -->

        <!-- START @WRAPPER -->
        <section id="wrapper">

		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
            <!-- START @HEADER -->
            <header id="header">

                <!-- Start header left -->
                <div class="header-left">
                    <!-- Start offcanvas left: This menu will take position at the top of template header (mobile only). Make sure that only #header have the `position: relative`, or it may cause unwanted behavior -->
                    <div class="navbar-minimize-mobile left">
                        <i class="fa fa-bars"></i>
                    </div>
                    <!--/ End offcanvas left -->

                    <!-- Start navbar header -->
                    <div class="navbar-header">

                        <!-- Start brand -->
                        <a class="navbar-brand" href="javascript:void(0);">
                            <img class="logo" src="<?php echo $this->webroot ?>img/logo-macro.png" style="height: 100%; padding: 5px;" alt="Banco Macro"/>
                        </a><!-- /.navbar-brand -->
                        <!--/ End brand -->

                    </div><!-- /.navbar-header -->
                    <!--/ End navbar header -->

                    <!-- Start offcanvas right: This menu will take position at the top of template header (mobile only). Make sure that only #header have the `position: relative`, or it may cause unwanted behavior -->
                    <div class="navbar-minimize-mobile right">
                        <i class="fa fa-cog"></i>
                    </div>
                    <!--/ End offcanvas right -->

                    <div class="clearfix"></div>
                </div><!-- /.header-left -->
                <!--/ End header left -->

                <!-- Start header right -->
                <div class="header-right">
                    <!-- Start navbar toolbar -->
                    <div class="navbar navbar-toolbar">

                        <!-- Start left navigation -->
                        <ul class="nav navbar-nav navbar-left">

                            <!-- Start sidebar shrink -->
                            <li class="navbar-minimize">
                                <a href="javascript:void(0);" title="Minimize sidebar">
                                    <i class="fa fa-bars"></i>
                                </a>
                            </li>
                            <!--/ End sidebar shrink -->


                        </ul>
                        <!--/ End left navigation -->

                        <!-- Start right navigation -->
                        <ul class="nav navbar-nav navbar-right"><!-- /.nav navbar-nav navbar-right -->

                        <!-- Start profile -->
                        <li class="dropdown navbar-profile">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <span class="meta">
                                    <span class="text hidden-xs hidden-sm text-muted"><?php echo $loggedUser['nombre'].' '.$loggedUser['apellido']; ?></span>
                                    <span class="caret"></span>
                                </span>
                            </a>
                            <!-- Start dropdown menu -->
                            <ul class="dropdown-menu animated flipInX">
                                <!--<li class="dropdown-header">Account</li>
                                <li><a href="page-profile.html"><i class="fa fa-user"></i>View profile</a></li>
                                <li><a href="mail-inbox.html"><i class="fa fa-envelope-square"></i>Inbox <span class="label label-info pull-right">30</span></a></li>
                                <li><a href="#"><i class="fa fa-share-square"></i>Invite a friend</a></li>
                                <li class="dropdown-header">Product</li>
                                <li><a href="#"><i class="fa fa-upload"></i>Upload</a></li>
                                <li><a href="#"><i class="fa fa-dollar"></i>Earning</a></li>
                                <li><a href="#"><i class="fa fa-download"></i>Withdrawals</a></li>
                                <li class="divider"></li>-->
                                <li><a href="<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'login')); ?>"><i class="fa fa-sign-out"></i>Salir</a></li>
                            </ul>
                            <!--/ End dropdown menu -->
                        </li>
                        <!--/ End profile -->

                        </ul>
                        <!--/ End right navigation -->

                    </div><!-- /.navbar-toolbar -->
                    <!--/ End navbar toolbar -->
                </div><!-- /.header-right -->
                <!--/ End header left -->

            </header> <!-- /#header -->
            <!--/ END HEADER -->

			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
            <aside id="sidebar-left" class="sidebar-circle">

                <!-- Start left navigation - profile shortcut -->
                <div class="sidebar-content">
                    <a href="#" class="close">&times;</a>
                    <div class="media">
                        <div class="media-body">
                            <h4 class="media-heading">Hola, <span><?php echo $loggedUser['nombre']; ?></span></h4>
                            <small>Administrador</small>
                        </div>
                    </div>
                </div><!-- /.sidebar-content -->
                <!--/ End left navigation -  profile shortcut -->

                <!-- Start left navigation - menu -->
                <ul class="sidebar-menu">
               
                    <li>
                        <a href="<?php echo $this->Html->Url(array('controller'=>'participantes','action'=>'index')); ?>">
                            <span class="icon"><i class="fa fa-group"></i></span>
                            <span class="text">Participantes</span>
                        </a>
                    </li>

                    <li class="submenu">
                        <a href="javascript:void(0);">
                            <span class="icon"><i class="fa fa-male"></i></span>
                            <span class="text">Usuarios</span>
                            <span class="arrow"></span>
                        </a>
                        <ul>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'index')); ?>">Usuarios existentes</a></li>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'agregarAdministrador')); ?>">Nuevo Administrador</a></li>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'agregarPromotora')); ?>">Nueva Promotora</a></li>
                        </ul>
                    </li>
					
					<li class="submenu">
                        <a href="javascript:void(0);">
                            <span class="icon"><i class="fa fa-list-alt"></i></span>
                            <span class="text">Eventos</span>
                            <span class="arrow"></span>
                        </a>
                        <ul>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'eventos','action'=>'index')); ?>">Eventos existentes</a></li>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'eventos','action'=>'agregar')); ?>">Eventos nuevos</a></li>
                        </ul>
                    </li>
					
					<li class="submenu">
                        <a href="javascript:void(0);">
                            <span class="icon"><i class="fa fa-tablet"></i></span>
                            <span class="text">Tablets</span>
                            <span class="arrow"></span>
                        </a>
                        <ul>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'tablets','action'=>'index')); ?>">Tablets existentes</a></li>
                            <li><a href="<?php echo $this->Html->Url(array('controller'=>'tablets','action'=>'agregar')); ?>">Tablets nuevas</a></li>
                        </ul>
                    </li>
					
					<li>
                        <a href="<?php echo $this->Html->Url(array('controller'=>'records','action'=>'index')); ?>">
                            <span class="icon"><i class="fa fa-pencil"></i></span>
                            <span class="text">Logs</span>
                        </a>
                    </li>

                </ul>
                <!--/ End left navigation - menu -->

                <!-- Start left navigation - footer -->
                <div class="sidebar-footer hidden-xs hidden-sm hidden-md">
                    <a id="fullscreen" class="pull-left" href="javascript:void(0);" data-toggle="tooltip" data-placement="top" data-title="Pantalla completa"><i class="fa fa-desktop"></i></a>
                    <a class="pull-left" href="<?php echo $this->Html->Url(array('controller'=>'usuarios','action'=>'login')); ?>" data-toggle="tooltip" data-placement="top" data-title="Salir"><i class="fa fa-power-off"></i></a>
                </div><!-- /.sidebar-footer -->
                <!--/ End left navigation - footer -->

            </aside><!-- /#sidebar-left -->
            <!--/ END SIDEBAR LEFT -->

			
			
			
			
			
			
			<style>
				.input.error{display: block!important;}
			</style>
			
			
			
			
			
			
			
            <!-- START @PAGE CONTENT -->
			<section id="page-content">
				<div class="header-content">
					<?php $icon = 'fa-file-o'; 
						switch($this->params['controller']){
							case 'usuarios':
							$icon = 'fa-male';
							break;
							case 'participantes':
							$icon = 'fa-group';
							break;
							case 'eventos':
							$icon = 'fa-list-alt';
							break;
							case 'configuraciones':
							$icon = 'fa-gear';
							break;
							case 'tablets':
							$icon = 'fa-tablets';
							break;
							case 'records':
							$icon = 'fa-pencil';
							break;
						}
					?>
					<h2><i class="fa <?php echo $icon; ?>"></i><?php echo $title_for_layout; ?><span><?php echo (isset($desc_for_layout)?$desc_for_layout:''); ?></span></h2>
					<!--<div class="breadcrumb-wrapper hidden-xs">
						<span class="label">You are here:</span>
						<ol class="breadcrumb">
							<li>
								<i class="fa fa-home"></i>
								<a href="dashboard.html">Dashboard</a>
								<i class="fa fa-angle-right"></i>
							</li>
							<li>
								<a href="#">Pages</a>
								<i class="fa fa-angle-right"></i>
							</li>
							<li class="active">Blank Page</li>
						</ol>
					</div>-->
				</div>
				<div class="body-content animated fadeIn">
					<div class="row">
						<div class="col-md-12">
							<div class="panel rounded shadow">
								<!--<div class="panel-heading">
									<h3 class="panel-title"><?php echo $title_for_layout; ?></h3>
								</div>-->
								<div class="panel-body">
									<?php echo $this->Session->flash(); ?>
									<?php echo $this->fetch('content'); ?>
								</div>
							</div>
						</div>
					</div>
				</div>
			</section>
			<!--/ END PAGE CONTENT -->
			
			
        </section><!-- /#wrapper -->
        <!--/ END WRAPPER -->

        <!-- START @BACK TOP -->
        <div id="back-top" class="animated pulse circle">
            <i class="fa fa-angle-up"></i>
        </div><!-- /#back-top -->
        <!--/ END BACK TOP -->

        <!-- START JAVASCRIPT SECTION (Load javascripts at bottom to reduce load time) -->
        <!-- START @CORE PLUGINS -->
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-cookie/jquery.cookie.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/typeahead/handlebars.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/typeahead/typeahead.bundle.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-niceScroll/jquery.nicescroll.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-sparkline/jquery.sparkline.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jpreloader-v2/js/jpreloader.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-easing/jquery.easing.1.3.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/ion-sound/js/ion.sound.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/bootbox/bootbox.js"></script>
        <!--/ END CORE PLUGINS -->

        <!-- START @PAGE LEVEL PLUGINS -->
        <script src="<?php echo $this->webroot; ?>js/bootstrap-datepicker.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/bootstrap-session-timeout/dist/bootstrap-session-timeout.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/flot/jquery.flot.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/flot/jquery.flot.spline.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/flot/jquery.flot.categories.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/flot/jquery.flot.tooltip.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/flot/jquery.flot.resize.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/flot/jquery.flot.pie.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/dropzone/dropzone.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/gritter/js/jquery.gritter.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/skycons/skycons.js"></script>
        <script>
            var icons = new Skycons({"color": "white"},{"resizeClear": true}),
                    list  = [
                        "clear-day", "clear-night", "partly-cloudy-day",
                        "partly-cloudy-night", "cloudy", "rain", "sleet", "snow", "wind",
                        "fog"
                    ],
                    i;

            for(i = list.length; i--; )
                icons.set(list[i], list[i]);

            icons.play();
        </script>
        <!--/ END PAGE LEVEL PLUGINS -->

        <!-- START @PAGE LEVEL SCRIPTS -->
        <script src="<?php echo $this->webroot . $theme ?>/admin/js/apps.js"></script>
        <!--<script src="<?php echo $this->webroot . $theme ?>/admin/js/pages/blankon.dashboard.js"></script>-->
        <script src="<?php echo $this->webroot . $theme ?>/admin/js/demo.js"></script>
        <!--/ END PAGE LEVEL SCRIPTS -->
        <!--/ END JAVASCRIPT SECTION -->

		<?php echo $this->element('sql_dump'); ?>
		
    </body>
    <!--/ END BODY -->
</html>