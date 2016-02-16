<!-- Login form -->
<?php echo $this->Form->create('Usuario',array('class'=>'sign-in form-horizontal shadow rounded no-overflow')); ?>
	<div class="sign-header">
		<div class="form-group">
			<div class="sign-text">
				<span>Administración</span>
			</div>
		</div>
	</div>
	<div class="sign-body">
		<div class="form-group">
			<div class="input-group input-group-lg rounded no-overflow">
				<input name="data[Usuario][username]" maxlength="250" id="UsuarioUsername" type="text" class="form-control input-sm" placeholder="Usuario">
				<span class="input-group-addon"><i class="fa fa-user"></i></span>
			</div>
		</div>
		<div class="form-group">
			<div class="input-group input-group-lg rounded no-overflow">
				<input name="data[Usuario][password]" id="UsuarioPassword" type="password" placeholder="Contraseña" class="form-control input-sm">
				<span class="input-group-addon"><i class="fa fa-lock"></i></span>
			</div>
			<span style="color:red;"><?php echo (isset($alerta)?$alerta:''); ?></span>
		</div>
	</div>
	<div class="sign-footer">
		<!--<div class="form-group">
			<div class="row">
				<div class="col-xs-6">
					<div class="ckbox ckbox-theme">
						<input id="rememberme" type="checkbox">
						<label for="rememberme" class="rounded">Remember me</label>
					</div>
				</div>
				<div class="col-xs-6 text-right">
					<a href="page-lost-password.html" title="lost password">Lost password?</a>
				</div>
			</div>
		</div>-->
		<div class="form-group">
			<button type="submit" class="btn btn-theme btn-lg btn-block no-margin rounded" id="login-btn">Ingresar</button>
		</div>
	</div>
<?php echo $this->Form->end(); ?>
<!--/ Login form -->