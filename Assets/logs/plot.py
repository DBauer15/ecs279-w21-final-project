import matplotlib.pyplot as plt
import numpy as np
import os
import glob

cols = ['generation', 'min', 'max', 'avg', 'med']
col_i = 3
ma_size = 50

def ma(x, w):
	return np.convolve(x, np.ones(w), 'valid') / w

extension = 'csv'
os.chdir('./')
result = glob.glob('*.{}'.format(extension))

fig, ax = plt.subplots()
data = dict()
for r in result:
	res = np.genfromtxt(r, delimiter=',', skip_header=1)
	if ma_size == 0:
		data[r] = res[:, col_i]
	else:
		data[r] = ma(res[:, col_i], ma_size)
	data[r] = ma(res[:, col_i], 10)
	x = np.arange(len(data[r]))
	label = r.replace('.csv', '')
	# label = ' '.join(label.split('_')[:3])
	ax.plot(x, data[r], label=label)
ax.xaxis.tick_bottom()
ax.legend()
if ma_size == 0:
	plt.title(f'{cols[col_i].capitalize()} Performance over Generations')
else:
	plt.title(f'{ma_size}-Generation Moving Avg of {cols[col_i].capitalize()} Performance over Generations')
plt.savefig(f'figure_{cols[col_i]}.png')
plt.show()
