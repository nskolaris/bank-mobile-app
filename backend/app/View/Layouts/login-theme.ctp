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
        <!--/ END PAGE LEVEL STYLES -->

        <!-- START @THEME STYLES -->
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/reset.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/layout.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/components.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/minified/plugins.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/themes/minified/default.theme.min.css" rel="stylesheet" id="theme">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/pages/minified/sign.min.css" rel="stylesheet">
        <link href="<?php echo $this->webroot . $theme ?>/admin/css/custom.css" rel="stylesheet">
        <!--/ END THEME STYLES -->

        <!-- START @IE SUPPORT -->
        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
        <!--/ END IE SUPPORT -->
    </head>
    <!--/ END HEAD -->

    <body style="background-color: rgb(0, 177, 225);">

        <!-- START @LOADING ANIMATION -->
        <div id="loading">
            <div class="loading-inner">
                <img class="animated bounceIn" src="<?php echo $this->webroot . $theme ?>/global/img/loader/flat/3.gif" alt="..."/>
            </div>
        </div>
        <!--/ END LOADING ANIMATION -->

        <!-- START @SIGN WRAPPER -->
        <div id="sign-wrapper">

			<!-- Brand -->
			<div class="brand">
				<img src="<?php echo $this->webroot ?>img/logo-macro.png" alt="brand logo" style="width: 100%;"/>
			</div>
			<!--/ Brand -->
		
			<?php echo $this->Session->flash(); ?>
			<?php echo $this->fetch('content'); ?>
		
        </div><!-- /#sign-wrapper -->
        <!--/ END SIGN WRAPPER -->

        <!-- START JAVASCRIPT SECTION (Load javascripts at bottom to reduce load time) -->
        <!-- START @CORE PLUGINS -->
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery/jquery.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-cookie/jquery.cookie.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jpreloader-v2/js/jpreloader.min.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-easing/jquery.easing.1.3.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/ion-sound/js/ion.sound.min.js"></script>
        <!--/ END CORE PLUGINS -->

        <!-- START @PAGE LEVEL PLUGINS -->
        <script src="<?php echo $this->webroot . $theme ?>/global/plugins/jquery-validation/jquery.validate.js"></script>
        <!--/ END PAGE LEVEL PLUGINS -->

        <!-- START @PAGE LEVEL SCRIPTS -->
        <script src="<?php echo $this->webroot . $theme ?>/admin/js/pages/blankon.sign.js"></script>
        <script src="<?php echo $this->webroot . $theme ?>/admin/js/demo.js"></script>
        <!--/ END PAGE LEVEL SCRIPTS -->
        <!--/ END JAVASCRIPT SECTION -->

    </body>
    <!-- END BODY -->

</html>