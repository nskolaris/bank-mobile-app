<script>
	function Cancelar(){
		window.location = "<?php echo $this->Html->Url(array('controller'=>'tablets','action'=>'index')); ?>";
	}
</script>
<?php 
echo $this->Form->create('Tablet');?><div class="form-body"><?php
	echo '<div class="form-group">'.$this->Form->input('Tablet.nombre',array('class'=>'form-control')).'</div>';
	echo '<div class="form-group">'.$this->Form->input('Tablet.code',array('class'=>'form-control','value'=>$code)).'</div>';
	echo '<div class="form-footer"><div class="pull-right"><button class="btn btn-danger" type="button" onClick="Cancelar();">Cancelar</button><button class="btn btn-success" type="submit">Guardar</button></div><div class="clearfix"></div></div>';
echo $this->Form->end();
?>